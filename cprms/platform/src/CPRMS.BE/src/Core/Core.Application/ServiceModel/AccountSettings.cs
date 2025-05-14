using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ServiceModel
{
    public class AccountSettings
    {
        public SuperAdminSettings SuperAdmin { get; set; }
        public List<PDaoDaoSettings> PDaoDao { get; set; }
    }
    public class SuperAdminSettings
    {
        public string Email { get; set; }
    }

    public class PDaoDaoSettings
    {
        public string Email { get; set; }
    }
}
