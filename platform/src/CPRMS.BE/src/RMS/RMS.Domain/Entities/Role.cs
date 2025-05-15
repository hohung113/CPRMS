using Core.Domain.Entities;

namespace Rms.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }
        public string? Description { get; set; }
    }
}
