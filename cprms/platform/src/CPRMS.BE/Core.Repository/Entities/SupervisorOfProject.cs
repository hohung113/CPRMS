namespace Core.Domain.Entities
{
    public class SupervisorOfProject : BaseEntity
    {
        public string FullName { get; set; }
        public string? Phone{ get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public bool IsMentor { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
