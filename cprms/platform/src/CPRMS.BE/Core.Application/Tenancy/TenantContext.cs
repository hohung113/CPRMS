using Core.Application.Interfaces;

namespace Core.Application.Tenancy
{
    public class TenantContext : ITenantSetter
    {
        public ITenantInfo CurrentTenant { get; private set; }

        public void SetCurrentTenant(ITenantInfo tenant)
        {
            CurrentTenant = tenant;
        }
    }
}
