using Core.Domain.Models.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Core.Api.Controller
{
    /*
        Run<T>(Func<Task<List<T>>>)  hỗ trợ trả về list.
        Run<T>(Func<Task<T>>)  cho từng item.
        Run(Func<Task<Guid>>)  cho các API kiểu tạo mới(POST) trả id.
        Run(Func<Task<bool>>)  cho các API kiểu Success/Fail(VD: delete, update status).
        Run<T>(Func<Task<BaseDataCollection<T>>>)  phân trang(pagination).
     */

    public static class ControllerBaseExtensions
    {
        public static async Task<BaseResponse<List<T>>> Run<T>(this ControllerBase controller, ILogger logger, Func<Task<List<T>>> method, bool IsView = false)
        {
            var methodName = method.Method.Name;
            logger.LogInformation("Entered method Run<T>");
            //try
            //{
            var data = await method();
            var result = AssembleResponse(data, IsView);
            logger.LogInformation("Leaving method Run<T>");
            return result;
            //}
            //catch (Exception e)
            //{
            //    return new BaseResponse<List<T>>()
            //    {
            //        Message = e.Message,
            //        ErrorCode = GetErrorCode(e),
            //        State = ResponseState.Error,
            //        CorrelationId = GenerateCorrelatedId(e, logger, methodName),
            //    };
            //}
        }
        public static async Task<BaseResponse<T>> Run<T>(this ControllerBase controller, ILogger logger, Func<Task<T>> method, bool IsView = false)
        {
            var methodName = method.Method.Name;
            logger.LogInformation("Entered method Run<T>");
            var data = await method();
            var result = AssembleResponse(data, IsView);
            logger.LogInformation("Leaving method Run<T>");
            return result;
        }

        public static async Task<BaseResponse<Guid>> Run(this ControllerBase controller, ILogger logger, Func<Task<Guid>> method, bool IsView = false)
        {
            var methodName = method.Method.Name;
            logger.LogInformation("Entered method Run");
            var data = await method();
            var result = AssembleResponse(data, IsView);
            logger.LogInformation("Leaving method Run");
            return result;
        }
        public static async Task<BaseResponse<bool>> Run(this ControllerBase controller, ILogger logger, Func<Task<bool>> method, bool IsView = false)
        {
            var methodName = method.Method.Name;
            logger.LogInformation("Entered method Run");
            var data = await method();
            var result = AssembleResponse(data, IsView);
            logger.LogInformation("Leaving method Run");
            return result;
        }
        public static async Task<BaseResponse<BaseDataCollection<T>>> Run<T>(this ControllerBase controller, ILogger logger, Func<Task<BaseDataCollection<T>>> method, bool IsView = false)
        {
            var methodName = method.Method.Name;
            logger.LogInformation("Entered method Run<T>");
            var data = await method();
            var result = AssembleResponse(data, IsView);
            logger.LogInformation("Leaving method Run<T>");
            return result;
        }
        private static BaseResponse<T> AssembleResponse<T>(T data, bool IsView)
        {
            var result = new BaseResponse<T>();
            result.Result = data;
            result.Message = "";
            result.State = ResponseState.Ok;
            result.HasPermission = true;
            result.IsView = IsView;
            return result;
        }
    }
}
