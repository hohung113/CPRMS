using Core.Utility.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using Rms.Application.Modules.Specialities.Dtos;
using Rms.Domain.Modules.Academic.Interface;

namespace Rms.Application.Modules.Specialities.Commands.CreateSpeciality
{
    public class CreateSpecialityCommandHandler : IRequestHandler<CreateSpecialityCommand, SpecialityDto>
    {
        private readonly ISpecialityRepository _specialityRepository;
        private readonly IProfessionRepository _professionRepository;
        private readonly ILogger<CreateSpecialityCommandHandler> _logger;
        public CreateSpecialityCommandHandler(
            ISpecialityRepository specialityRepository,
            IProfessionRepository professionRepository,
            ILogger<CreateSpecialityCommandHandler> logger)
        {
            _specialityRepository = specialityRepository;
            _professionRepository = professionRepository;
            _logger = logger;
        }

        public async Task<SpecialityDto> Handle(CreateSpecialityCommand request, CancellationToken cancellationToken)
        {
            var profession = await _professionRepository.GetEntity(request.ProfessionId);
            if (profession == null)
                throw new NotFoundException($"Profession with ID {request.ProfessionId} not found");

            var specialityEntity = request.Adapt<Rms.Domain.Entities.Speciality>();
            var createdSpeciality = await _specialityRepository.AddEntity(specialityEntity);

            _logger.LogInformation("Created Speciality: Id={SpecialityId}", createdSpeciality.Id);

            return createdSpeciality.Adapt<SpecialityDto>();
        }

    }
}
