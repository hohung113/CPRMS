using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using Core.Utility.Exceptions;

namespace Core.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            if (exception is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
                result = JsonSerializer.Serialize(new
                {
                    error = exception.Message,
                    code = 404
                });
            }
            else if (exception is BaseException baseEx)
            {
                code = (HttpStatusCode)baseEx.ErrorCode;
                result = JsonSerializer.Serialize(new
                {
                    error = baseEx.Message,
                    code = baseEx.ErrorCode
                });
            }
            else
            {
                result = JsonSerializer.Serialize(new
                {
                    error = "An unexpected error occurred."
                });
            }

            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
