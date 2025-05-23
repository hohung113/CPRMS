using Rms.Application.Modules.Acedamic.Professions.Dtos;

namespace Rms.Application.Modules.Acedamic.Professions.Commands.UpdateProfession
{
    public class UpdateProfessionCommand : IRequest<ProfessionDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid? LastModifiedBy { get; set; }
    }
}
