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
        public string? CurriculumName { get; set; }
        public Guid? SpecialityId { get; set; } // ex : Software Enginner
        public Guid SemesterId { get; set; }
        public virtual Speciality? Speciality { get; set; }
        public virtual Semester? Semester { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
