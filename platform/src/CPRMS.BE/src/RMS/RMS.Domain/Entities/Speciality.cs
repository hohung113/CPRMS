namespace Rms.Domain.Entities
{
    public class Speciality : BaseEntity
    {
        public string Name { get; set; }
        public Guid ProfessionId { get; set; }
        public virtual Profession Profession { get; set; }
        public virtual ICollection<UserSystem>? UserSystems { get; set; }
    }
}
