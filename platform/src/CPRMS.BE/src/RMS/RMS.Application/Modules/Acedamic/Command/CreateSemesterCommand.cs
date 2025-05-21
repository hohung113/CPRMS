namespace Rms.Application.Modules.Acedamic.Command
{
    public class CreateSemesterCommand : IRequest<SemesterDto> // IRequest<TResponse>
    {
        public string Name { get; set; }
        public string? Description  { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        // Add other properties needed to create a semester

        public CreateSemesterCommand(string name, DateTimeOffset startDate, DateTimeOffset endDate, string? description)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
        }
    }
}