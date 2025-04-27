namespace Core.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Code { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Campus { get; set; }
        public bool IsBlock { get; set; }
        public Guid GroupId { get; set; }
        public Guid RoleId { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
