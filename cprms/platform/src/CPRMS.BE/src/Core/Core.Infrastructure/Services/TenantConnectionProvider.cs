using Core.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Core.Infrastructure.Services
{
    public class TenantConnectionProvider : ITenantConnectionProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        public TenantConnectionProvider(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        public string GetConnectionString(string dbName)
        {
            var tenantId = _httpContextAccessor.HttpContext?.Items["TenantId"]?.ToString()
            ?? throw new InvalidOperationException("Tenant ID not found");

            var baseConnectionString = _configuration.GetConnectionString(dbName)
                ?? throw new InvalidOperationException($"Connection string for {dbName} not found");

            return baseConnectionString.Replace($"database={dbName.ToLower()}", $"database={tenantId}_{dbName.ToLower()}");
        }
    }
}
