namespace Rms.Application.Modules.Specialities.Commands.DeleteSpeciality
{
    public class DeleteSpecialityCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProfessionId { get; set; }
    }
}
