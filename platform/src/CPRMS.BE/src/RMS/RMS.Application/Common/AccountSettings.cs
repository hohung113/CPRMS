namespace Rms.Application.Common
{
    public class AccountSettings
    {
        public AdminSettings Admin { get; set; }
        public List<AcademicOfficeSettings> AcademicOffice { get; set; }
    }
    public class AdminSettings
    {
        public string Email { get; set; }
    }

    public class AcademicOfficeSettings
    {
        public string Email { get; set; }
    }
}
