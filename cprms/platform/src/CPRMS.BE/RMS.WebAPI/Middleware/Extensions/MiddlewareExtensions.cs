namespace RMS.WebAPI.Middleware.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder app)
        {
            return app
                .UseMiddleware<CorrelationIdMiddleware>()
                .UseMiddleware<RequestLoggingMiddleware>()
                .UseMiddleware<ExceptionHandlingMiddleware>()
                .UseMiddleware<ValidationMiddleware>();
        }
    }
}
