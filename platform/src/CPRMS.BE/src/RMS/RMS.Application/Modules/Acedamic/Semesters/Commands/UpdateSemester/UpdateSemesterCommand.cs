using Rms.Application.Modules.Acedamic.Semesters.Dtos;

namespace Rms.Application.Modules.Acedamic.Semesters.Commands.UpdateSemester
{
    public class UpdateSemesterCommand : IRequest<SemesterDto>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public UpdateSemesterCommand(string name, DateTimeOffset startDate, DateTimeOffset endDate, string? description)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
        }
    }
}