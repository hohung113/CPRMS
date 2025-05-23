using Rms.Application.Modules.Acedamic.Semesters.Dtos;

namespace Rms.Application.Modules.Acedamic.Semesters.Commands.CreateSemester
{
    public class CreateSemesterCommand : IRequest<SemesterDto>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public CreateSemesterCommand(string name, DateTimeOffset startDate, DateTimeOffset endDate, string? description)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
        }
    }
}
