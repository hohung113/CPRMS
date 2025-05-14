using Core.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Tenancy
{
    public class AppTenantInfo : ITenantInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SchemaName { get; set; }
    }
}
