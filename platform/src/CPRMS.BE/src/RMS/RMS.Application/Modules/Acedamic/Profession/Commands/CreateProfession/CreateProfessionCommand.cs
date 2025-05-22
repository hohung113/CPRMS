using Rms.Application.Modules.Acedamic.Profession.Dtos;

namespace Rms.Application.Modules.Acedamic.Profession.Commands.CreateProfession
{
    public class CreateProfessionCommand : IRequest<ProfessionDto>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public CreateProfessionCommand(string name, string code)
        {
            this.Name = name;
            this.Code = code;
        }
    }
}
