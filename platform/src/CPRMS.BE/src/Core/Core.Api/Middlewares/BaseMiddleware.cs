using Microsoft.AspNetCore.Http;

namespace Core.Api.Middlewares
{
    public abstract class BaseMiddleware
    {
        private readonly RequestDelegate _next;

        protected BaseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (!await HandleAsync(context))
                {
                    return;
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        protected abstract Task<bool> HandleAsync(HttpContext context);
        protected virtual Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = 500;
            return context.Response.WriteAsync("Internal server error.");
        }
    }

}
