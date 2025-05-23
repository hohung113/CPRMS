using Rms.Application.Modules.Specialities.Dtos;

namespace Rms.Application.Modules.Specialities.Commands.CreateSpeciality
{
    public class CreateSpecialityCommand : IRequest<SpecialityDto>
    {
        public string Name { get; set; }
        public Guid ProfessionId { get; set; }
        public CreateSpecialityCommand(string name, Guid professionId)
        {
            this.Name = name;
            this.ProfessionId = professionId;
        }
    }
}
