using MediatR;
using Microsoft.Extensions.Logging;
using Rms.Application.Modules.Acedamic.Professions.Commands.CreateProfession;
using Rms.Application.Modules.Acedamic.Professions.Dtos;
using Rms.Domain.Modules.Academic.Interface;

namespace Rms.Application.Modules.Acedamic.Profession.Commands.CreateProfession
{
    public class CreateProfessionCommandHandler : IRequestHandler<CreateProfessionCommand, ProfessionDto>
    {
        private readonly IProfessionRepository _professionRepository;
        private readonly ILogger<CreateProfessionCommandHandler> _logger;

        public CreateProfessionCommandHandler(
            IProfessionRepository professionRepository,
            ILogger<CreateProfessionCommandHandler> logger)
        {
            _professionRepository = professionRepository;
            _logger = logger;
        }

        public async Task<ProfessionDto> Handle(CreateProfessionCommand request, CancellationToken cancellationToken)
        {
            var professionEntity = request.Adapt<Rms.Domain.Entities.Profession>();
            var createdProfession = await _professionRepository.AddEntity(professionEntity);
            if (createdProfession == null)
            {
                _logger.LogError("Failed to create profession: {@Request}", request);
                throw new Exception("Failed to create profession in the repository.");
            }

            _logger.LogInformation("Profession created successfully: {ProfessionId}", createdProfession.Id);

            var professionDto = createdProfession.Adapt<ProfessionDto>();
            return professionDto;
        }
    }

}
