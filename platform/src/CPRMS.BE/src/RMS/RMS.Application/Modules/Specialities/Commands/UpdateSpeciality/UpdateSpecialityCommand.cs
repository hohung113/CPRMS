using Rms.Application.Modules.Specialities.Dtos;

namespace Rms.Application.Modules.Specialities.Commands.UpdateSpeciality
{
    public class UpdateSpecialityCommand : IRequest<SpecialityDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProfessionId { get; set; }
    }
}
