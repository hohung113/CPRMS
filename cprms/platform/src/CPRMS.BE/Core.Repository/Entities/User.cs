using Core.Domain.Models.Enums;

namespace Core.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Code { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public Campus Campus { get; set; }
        public bool IsBlock { get; set; }
        public Guid CurriculumnId { get; set; } 
    }
}
