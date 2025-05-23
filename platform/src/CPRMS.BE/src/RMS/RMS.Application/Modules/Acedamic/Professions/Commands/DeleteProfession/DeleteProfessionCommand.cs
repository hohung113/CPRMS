namespace Rms.Application.Modules.Acedamic.Professions.Commands.DeleteProfession
{
        public class DeleteProfessionCommand : IRequest<bool>
        {
            public Guid Id { get; set; }
        }
}
