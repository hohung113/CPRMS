using Core.Application.Interfaces;
using Core.Application.Tenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Services
{
    public class DatabaseTenantStore : ITenantStore
    {
        //private readonly AdminDbContext _adminDbContext; // DbContext for tenantid. mongo DB

        //public DatabaseTenantStore(AdminDbContext adminDbContext)
        //{
        //    _adminDbContext = adminDbContext;
        //}
        public Task<ITenantInfo> GetTenantAsync(string tenantIdentifier)
        {
            var demoTenantId = "123e4567-e89b-12d3-a456-426614174000";
            var demoTenantName = "Demo Tenant";
            var demoSchemaName = "tenant_schema__123e4567";

            if (tenantIdentifier == demoTenantId)
            {
                return Task.FromResult<ITenantInfo>(new AppTenantInfo
                {
                    Id = demoTenantId,
                    Name = demoTenantName,
                    SchemaName = demoSchemaName
                });
            }
            return Task.FromResult<ITenantInfo>(null);
        }
    }
}
