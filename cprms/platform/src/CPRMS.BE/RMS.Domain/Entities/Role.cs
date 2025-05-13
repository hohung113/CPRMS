using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rms.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }
        public string? Description { get; set; }
    }
}
