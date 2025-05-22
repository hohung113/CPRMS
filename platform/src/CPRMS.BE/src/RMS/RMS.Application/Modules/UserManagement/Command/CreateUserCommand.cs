namespace Rms.Application.Modules.UserManagement.Command
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public string Code { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public Campus Campus { get; set; }
        public bool IsBlock { get; set; }
        public string? ProfileImage { get; set; }
        public string? CurriculumName { get; set; }
        public Guid? SpecialityId { get; set; }
        public Guid? SemesterId { get; set; }

        public CreateUserCommand()
        {
            
        }
        public CreateUserCommand(
       string code,
       string email,
       string fullName,
       Campus campus,
       bool isBlock = false,
       string profileImage = "",
       string? curriculumName = null,
       Guid? specialityId = null,
       Guid? semesterId = null)
        {
            Code = code;
            Email = email;
            FullName = fullName;
            Campus = campus;
            IsBlock = isBlock;
            ProfileImage = profileImage;
            CurriculumName = curriculumName;
            SpecialityId = specialityId;
            SemesterId = semesterId;
        }
    }
}