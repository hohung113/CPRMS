using Rms.Domain.Modules.Academic.Interface;
using MediatR;
using Core.Utility.Exceptions;
using Microsoft.Extensions.Logging;

namespace Rms.Application.Modules.Acedamic.Professions.Commands.DeleteProfession
{
    public class DeleteProfessionCommandHandler : IRequestHandler<DeleteProfessionCommand, bool>
    {
        private readonly IProfessionRepository _professionRepository;
        private readonly ILogger<DeleteProfessionCommandHandler> _logger;

        public DeleteProfessionCommandHandler(
            IProfessionRepository professionRepository,
            ILogger<DeleteProfessionCommandHandler> logger)
        {
            _professionRepository = professionRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteProfessionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _professionRepository.GetEntity(request.Id);
            if (entity == null)
            {
                _logger.LogWarning("Attempted to delete non-existent profession: {ProfessionId}", request.Id);
                throw new NotFoundException($"Profession with ID {request.Id} not found");
            }

            var success = await _professionRepository.DeleteEntity(entity);

            if (success)
                _logger.LogInformation("Profession deleted successfully: {ProfessionId}", request.Id);
            else
                _logger.LogError("Failed to delete profession: {ProfessionId}", request.Id);

            return success;
        }
    }

}
