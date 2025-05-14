using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Middlewares
{
    public class TenantResolutionMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantResolutionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var tenant = context.Request.Headers["X-Tenant"].ToString();

            if (string.IsNullOrEmpty(tenant))
            {
                tenant = "default";
            }
            context.Items["Tenant"] = tenant;
            await _next(context);
        }
    }
}