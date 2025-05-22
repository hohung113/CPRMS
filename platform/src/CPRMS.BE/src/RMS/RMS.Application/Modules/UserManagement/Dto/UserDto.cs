namespace Rms.Application.Modules.UserManagement.Dto
{
    public class UserDto
    {
        public string Code { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public Campus? Campus { get; set; }
        public bool IsBlock { get; set; }
        public string? ProfileImage { get; set; }
        public string? CurriculumName { get; set; }
        public Guid? SpecialityId { get; set; }
        public Guid? SemesterId { get; set; }
    }
}
