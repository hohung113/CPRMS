//using Core.CPRMS.Constants;
//using Core.Domain.BaseRepo;
//using Core.Domain.Models.Base;
//using Core.Utility.Enums;
//using Core.Utility.Exceptions;
//using Core.Utility.Json;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Diagnostics;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Primitives;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Reflection.Metadata;
//using System.Text;
//using System.Threading.Tasks;

//namespace Core.Api.Middlewares
//{
//    public class GlobalExceptionHandlerMiddleware
//    {
//        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
//        private readonly RequestDelegate _next;

//        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
//        {
//            _next = next;
//            _logger = logger;
//        }

//        public async Task Invoke(HttpContext httpContext)
//        {
//            IExceptionHandlerPathFeature? exceptionHandlerPathFeature = httpContext.Features.Get<IExceptionHandlerPathFeature>();
//            if (exceptionHandlerPathFeature == null || exceptionHandlerPathFeature.Error == null)
//            {
//                return;
//            }

//            // Xóa phần correlationId tạm thời
//            // string correlationId = GetCorrelationId(httpContext);
//            // LogContext.PushProperty("CorrelationId", correlationId);

//            _logger.LogError(exceptionHandlerPathFeature.Error, "Exception Handler Running.");

//#if DEBUG
//            httpContext.Response.Headers.TryAdd("Access-Control-Allow-Origin", httpContext.Request.Headers["Origin"]);
//#endif

//            httpContext.Response.ContentType = "application/json";
//            httpContext.Response.StatusCode = 200;

//            // Xóa correlationId khỏi response body
//            string context = BuildExceptionResponse(exceptionHandlerPathFeature.Error);
//            await httpContext.Response.WriteAsync(context);
//        }

//        private static string BuildExceptionResponse(Exception ex)
//        {
//            BaseResponse<object?> result;
//            //string message = CoreConstants.CommonErrorMessage;
//            string message = "Global error";

//#if DEBUG
//            message = ex.Message;
//#endif

//            if (ex is BaseException b)
//            {
//                result = new BaseResponse<object?>()
//                {
//                    Result = null,
//                    State = ResponseState.Error,
//                    Message = message,
//                    ErrorCode = (ErrorCode)b.ErrorCode,
//                    //CorrelationId
//                    CorrelationId = Guid.Empty
//                };
//            }
//            else
//            {
//                result = new BaseResponse<object?>()
//                {
//                    Result = null,
//                    State = ResponseState.Error,
//                    Message = message,
//                    ErrorCode = 0,
//                    CorrelationId = Guid.Empty 
//                };
//            }

//            string responseBody = JsonSerializerHelper.Serialize(result);
//            return responseBody;
//        }

//        // Get GetCorrelationId
//        // private static string GetCorrelationId(HttpContext httpcontext)
//        // {
//        //     if (httpcontext.Items.TryGetValue(Repository.CoreConstants.CorrelationIdRequestHeader, out object? correlationId) && correlationId != default)
//        //     {
//        //         string correlationIdstring = correlationId.ToString() ?? String.Empty;
//        //         httpcontext.Request.Headers[Repository.CoreConstants.CorrelationIdRequestHeader] = correlationIdstring;
//        //         return correlationIdstring;
//        //     }
//        //     if (httpcontext.Response.Headers.TryGetValue(Repository.CoreConstants.CorrelationIdRequestHeader, out StringValues responseValue))
//        //     {
//        //         return responseValue.FirstOrDefault() ?? String.Empty;
//        //     }
//        //     if (httpcontext.Request.Headers.TryGetValue(Repository.CoreConstants.CorrelationIdRequestHeader, out StringValues requestValue))
//        //     {
//        //         return requestValue.FirstOrDefault() ?? String.Empty;
//        //     }
//        //     return String.Empty;
//        // }
//    }

//    // Extension method used to add the middleware to the HTTP request pipeline.
//    public static class GlobalExceptionHandlerMiddlewareExtensions
//    {
//        /// <summary>
//        /// Global Exception Handler. This is a Middleware which is only used in "ExceptionHandler Middleware".
//        /// Whenever any exception is thrown by request anywhere, UseExceptionHandler will create an alternate request pipeline automatically to handle exception.
//        /// The following middleware will directly short-circuit the request pipeline and write the predefined exception body to the response.
//        /// </summary>
//        /// <param name="builder"></param>
//        /// <returns></returns>
//        public static IApplicationBuilder UseGlobalExceptionHandlerMiddleware(this IApplicationBuilder builder)
//        {
//            return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
//        }
//    }
//}