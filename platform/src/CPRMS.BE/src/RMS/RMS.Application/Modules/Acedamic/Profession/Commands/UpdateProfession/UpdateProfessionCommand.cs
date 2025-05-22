namespace Rms.Application.Modules.Acedamic.Profession.Commands.UpdateProfession
{
    public class UpdateProfessionCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid LastModifiedBy { get; set; }
    }
}
