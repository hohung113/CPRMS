using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rms.Infrastructure.Persistence
{
    public interface ICampusProvider
    {
        string CampusName { get; set; }
    }

    public class CampusProvider : ICampusProvider
    {
        public string CampusName { get; set; }
    }
}
