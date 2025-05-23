using Core.Utility.Exceptions;
using MediatR;
using Rms.Domain.Modules.Academic.Interface;

namespace Rms.Application.Modules.Specialities.Commands.DeleteSpeciality
{
    public class DeleteSpecialityCommandHandler : IRequestHandler<DeleteSpecialityCommand, bool>
    {
        private readonly ISpecialityRepository _specialityRepository;

        public DeleteSpecialityCommandHandler(ISpecialityRepository specialityRepository)
        {
            _specialityRepository = specialityRepository;
        }

        public async Task<bool> Handle(DeleteSpecialityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _specialityRepository.GetEntity(request.Id);
            if (entity == null)
                throw new NotFoundException($"Speciality with ID {request.Id} not found");

            return await _specialityRepository.DeleteEntity(entity);
        }
    }

}
