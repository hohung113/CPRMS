using MediatR;
using Rms.Application.Modules.Acedamic.Profession.Dtos;
using Rms.Domain.Modules.Academic.Interface;

namespace Rms.Application.Modules.Acedamic.Profession.Commands.CreateProfession
{
    public class CreateProfessionCommandHandler : IRequestHandler<CreateProfessionCommand, ProfessionDto>
    {
        private readonly IProfessionRepository _professionRepository;
        public CreateProfessionCommandHandler(IProfessionRepository professionRepository)
        {
            _professionRepository = professionRepository;
        }


        public async Task<ProfessionDto> Handle(CreateProfessionCommand request, CancellationToken cancellationToken)
        {
            var professionEntity = request.Adapt<Rms.Domain.Entities.Profession>();
            var createdProfession = await _professionRepository.AddEntity(professionEntity);
            if (createdProfession == null)
            {
                throw new System.Exception("Failed to create profession in the repository.");
            }
            var professionDto = createdProfession.Adapt<ProfessionDto>();
            return await Task.FromResult(professionDto);
        }
    }
}
