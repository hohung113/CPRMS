using Core.Application.Interfaces;

namespace Gateway.GlobalMiddleware
{
    public class TenantResolutionMiddleware
    {
        private readonly ILogger<TenantResolutionMiddleware> _logger;
        private readonly RequestDelegate _next;
        public TenantResolutionMiddleware(RequestDelegate next, ILogger<TenantResolutionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, ITenantStore tenantStore, ITenantSetter tenantSetter)
        {

            if (context.Request.Headers.TryGetValue("X-Tenant-ID", out var tenantIdHeaderValue))
            {
                var tenantIdentifier = tenantIdHeaderValue.FirstOrDefault();

                if (!string.IsNullOrEmpty(tenantIdentifier))
                {
                    _logger.LogInformation("Resolving tenant: {TenantId}", tenantIdentifier);
                    // Check Tenant From Mongo - Dbv
                    var tenant = await tenantStore.GetTenantAsync(tenantIdentifier);

                    if (tenant != null)
                    {
                        // Tenant Current
                        tenantSetter.SetCurrentTenant(tenant);
                    }
                    else
                    {
                        // Tenant InValid
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await context.Response.WriteAsync("Invalid Tenant ID.");
                        return;
                    }
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Tenant ID header value is missing.");
                    return;
                }
            }
            else
            {

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Tenant ID header is required.");
                return;
            }

            await _next(context);
        }
    }

    public static class TenantResolutionMiddlewareExtensions
    {
        public static IApplicationBuilder UseTenantResolution(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TenantResolutionMiddleware>();
        }
    }
}
