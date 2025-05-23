using Core.Utility.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using Rms.Application.Modules.Acedamic.Professions.Dtos;
using Rms.Domain.Modules.Academic.Interface;

namespace Rms.Application.Modules.Acedamic.Professions.Commands.UpdateProfession
{
    public class UpdateProfessionCommandHandler : IRequestHandler<UpdateProfessionCommand, ProfessionDto>
    {
        private readonly IProfessionRepository _professionRepository;
        private readonly ILogger<UpdateProfessionCommandHandler> _logger;

        public UpdateProfessionCommandHandler(
            IProfessionRepository professionRepository,
            ILogger<UpdateProfessionCommandHandler> logger)
        {
            _professionRepository = professionRepository;
            _logger = logger;
        }

        public async Task<ProfessionDto> Handle(UpdateProfessionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _professionRepository.GetEntity(request.Id);
            if (entity == null)
            {
                _logger.LogWarning("Profession not found: {ProfessionId}", request.Id);
                throw new NotFoundException($"Profession with ID {request.Id} not found");
            }

            entity.Name = request.Name;
            entity.Code = request.Code;

            await _professionRepository.UpdateEntity(entity);

            _logger.LogInformation("Profession updated successfully: {ProfessionId}", entity.Id);

            return entity.Adapt<ProfessionDto>();
        }
    }

}
