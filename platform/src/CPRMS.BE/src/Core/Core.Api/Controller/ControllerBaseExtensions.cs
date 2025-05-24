using Core.Domain.Models.Base;
using Core.Utility.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
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
        public static async Task<BaseResponse<List<T>>> Run<T>(
            this ControllerBase controller,
            ILogger logger,
            Func<Task<List<T>>> method,
            bool IsView = false)
        {
            return await ExecuteWithErrorHandling(logger, method, IsView);
        }

        public static async Task<BaseResponse<T>> Run<T>(
            this ControllerBase controller,
            ILogger logger,
            Func<Task<T>> method,
            bool IsView = false)
        {
            return await ExecuteWithErrorHandling(logger, method, IsView);
        }

        public static async Task<BaseResponse<Guid>> Run(
            this ControllerBase controller,
            ILogger logger,
            Func<Task<Guid>> method,
            bool IsView = false)
        {
            return await ExecuteWithErrorHandling(logger, method, IsView);
        }

        public static async Task<BaseResponse<bool>> Run(
            this ControllerBase controller,
            ILogger logger,
            Func<Task<bool>> method,
            bool IsView = false)
        {
            return await ExecuteWithErrorHandling(logger, method, IsView);
        }

        public static async Task<BaseResponse<BaseDataCollection<T>>> Run<T>(
            this ControllerBase controller,
            ILogger logger,
            Func<Task<BaseDataCollection<T>>> method,
            bool IsView = false)
        {
            return await ExecuteWithErrorHandling(logger, method, IsView);
        }

        private static async Task<BaseResponse<T>> ExecuteWithErrorHandling<T>(
            ILogger logger,
            Func<Task<T>> method,
            bool isView)
        {
            var correlationId = Guid.NewGuid();
            var methodName = method.Method.Name;
            var stopwatch = Stopwatch.StartNew();

            logger.LogInformation("Started method {MethodName} with CorrelationId {CorrelationId}",
                methodName, correlationId);

            try
            {
                var data = await method();
                stopwatch.Stop();

                logger.LogInformation("Completed method {MethodName} successfully in {ElapsedMs}ms with CorrelationId {CorrelationId}",
                    methodName, stopwatch.ElapsedMilliseconds, correlationId);

                return AssembleResponse(data, isView, correlationId);
            }
            catch (ArgumentException ex)
            {
                stopwatch.Stop();
                logger.LogWarning(ex, "Validation error in method {MethodName} after {ElapsedMs}ms with CorrelationId {CorrelationId}: {Message}",
                    methodName, stopwatch.ElapsedMilliseconds, correlationId, ex.Message);

                return AssembleErrorResponse<T>(
                    ErrorCode.ValidationError,
                    ex.Message,
                    isView,
                    correlationId);
            }
            catch (UnauthorizedAccessException ex)
            {
                stopwatch.Stop();
                logger.LogWarning(ex, "Unauthorized access in method {MethodName} after {ElapsedMs}ms with CorrelationId {CorrelationId}: {Message}",
                    methodName, stopwatch.ElapsedMilliseconds, correlationId, ex.Message);

                return AssembleErrorResponse<T>(
                    ErrorCode.Forbidden,
                    ex.Message,
                    isView,
                    correlationId,
                    hasPermission: false);
            }
            catch (KeyNotFoundException ex)
            {
                stopwatch.Stop();
                logger.LogWarning(ex, "Resource not found in method {MethodName} after {ElapsedMs}ms with CorrelationId {CorrelationId}: {Message}",
                    methodName, stopwatch.ElapsedMilliseconds, correlationId, ex.Message);

                return AssembleErrorResponse<T>(
                    ErrorCode.NotFound,
                    ex.Message,
                    isView,
                    correlationId);
            }
            catch (TimeoutException ex)
            {
                stopwatch.Stop();
                logger.LogError(ex, "Timeout in method {MethodName} after {ElapsedMs}ms with CorrelationId {CorrelationId}: {Message}",
                    methodName, stopwatch.ElapsedMilliseconds, correlationId, ex.Message);

                return AssembleErrorResponse<T>(
                    ErrorCode.Timeout,
                    "Request timeout",
                    isView,
                    correlationId);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                logger.LogError(ex, "Unexpected error in method {MethodName} after {ElapsedMs}ms with CorrelationId {CorrelationId}: {Message}",
                    methodName, stopwatch.ElapsedMilliseconds, correlationId, ex.Message);

                return AssembleErrorResponse<T>(
                    ErrorCode.InternalError,
                    "An internal error occurred",
                    isView,
                    correlationId);
            }
        }

        private static BaseResponse<T> AssembleResponse<T>(T data, bool isView, Guid correlationId)
        {
            return new BaseResponse<T>
            {
                Result = data,
                Message = GetSuccessMessage<T>(),
                State = ResponseState.Ok,
                HasPermission = true,
                IsView = isView,
                CorrelationId = correlationId
            };
        }

        private static BaseResponse<T> AssembleErrorResponse<T>(
            ErrorCode errorCode,
            string message,
            bool isView,
            Guid correlationId,
            bool hasPermission = true)
        {
            return new BaseResponse<T>
            {
                Result = default(T),
                Message = message,
                State = ResponseState.Error,
                ErrorCode = errorCode,
                HasPermission = hasPermission,
                IsView = isView,
                CorrelationId = correlationId
            };
        }

        private static string GetSuccessMessage<T>()
        {
            var type = typeof(T);

            if (type == typeof(bool))
                return "Operation completed successfully";

            if (type == typeof(Guid))
                return "Resource created successfully";

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
                return "Data retrieved successfully";

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(BaseDataCollection<>))
                return "Data retrieved successfully";

            return "Success";
        }
    }
}