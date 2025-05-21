namespace Rms.Domain.Entities
{
    public class Semester : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

    }
}
