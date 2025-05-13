using Core.Domain.Entities;
using Rms.Domain.Enums;

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
