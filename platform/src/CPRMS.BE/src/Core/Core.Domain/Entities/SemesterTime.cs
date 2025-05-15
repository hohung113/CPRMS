namespace Core.Domain.Entities
{
    public class SemesterTime : BaseEntity
    {
        public string SemesterName { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
    }
}
