using Core.Utility.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using Rms.Application.Modules.Specialities.Dtos;
using Rms.Domain.Modules.Academic.Interface;

namespace Rms.Application.Modules.Specialities.Commands.UpdateSpeciality
{
    public class UpdateSpecialityCommandHandler : IRequestHandler<UpdateSpecialityCommand, SpecialityDto>
    {
        private readonly ISpecialityRepository _specialityRepository;
        private readonly IProfessionRepository _professionRepository;
        private readonly ILogger<UpdateSpecialityCommandHandler> _logger;

        public UpdateSpecialityCommandHandler(
            ISpecialityRepository specialityRepository,
            IProfessionRepository professionRepository,
            ILogger<UpdateSpecialityCommandHandler> logger)
        {
            _specialityRepository = specialityRepository;
            _professionRepository = professionRepository;
            _logger = logger;
        }

        public async Task<SpecialityDto> Handle(UpdateSpecialityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _specialityRepository.GetEntity(request.Id);
            if (entity == null)
                throw new NotFoundException($"Speciality with ID {request.Id} not found");

            var profession = await _professionRepository.GetEntity(request.ProfessionId);
            if (profession == null)
                throw new NotFoundException($"Profession with ID {request.ProfessionId} not found");

            entity.Name = request.Name;
            entity.ProfessionId = request.ProfessionId;

            await _specialityRepository.UpdateEntity(entity);
            return entity.Adapt<SpecialityDto>();
        }

    }

}
