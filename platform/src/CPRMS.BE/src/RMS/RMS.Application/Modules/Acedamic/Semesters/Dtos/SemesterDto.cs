namespace Rms.Application.Modules.Acedamic.Semesters.Dtos
{
    public class SemesterDto : CommonDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}
