namespace Rms.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
