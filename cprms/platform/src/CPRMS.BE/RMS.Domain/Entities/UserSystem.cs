using Core.Domain.Entities;
using Core.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rms.Domain.Entities
{
    public class UserSystem : BaseEntity
    {
        public string Code { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public Campus Campus { get; set; }
        public bool IsBlock { get; set; }
        public string ProfileImage { get; set; }
        public Guid? CurriculumnId { get; set; }
    }
}
