namespace Gateway.GlobalMiddleware
{
    public class TenantValidationMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        public TenantValidationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var appId = context.Request.Path.Value?.Split('/')[2];
            if (string.IsNullOrEmpty(appId) || !Guid.TryParse(appId, out var tenantId))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid or missing appId");
                return;
            }

            var tenants = _configuration.GetSection("Tenants").Get<List<Tenant>>();
            if (tenants == null || !tenants.Any(t => t.TenantId == tenantId.ToString()))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Invalid tenant");
                return;
            }
            context.Items["TenantId"] = tenantId.ToString();

            await _next(context);
        }

        public class Tenant
        {
            public string TenantId { get; set; }
            public string Name { get; set; }
        }
    }
}
