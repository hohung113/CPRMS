using Core.Api.MediatRCustom;
using Core.Utility.Exceptions;
using MediatR;
using Rms.Domain.Modules.Academic.Interface;

namespace Rms.Application.Modules.Acedamic.Profession.Commands.UpdateProfession
{
    public class UpdateProfessionCommandHandler : IRequestHandler<UpdateProfessionCommand, Unit>
    {
        private readonly IProfessionRepository _professionRepository;

        public UpdateProfessionCommandHandler(IProfessionRepository professionRepository)
        {
            _professionRepository = professionRepository;
        }

        public async Task<Unit> Handle(UpdateProfessionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _professionRepository.GetEntity(request.Id);
            if (entity == null)
                throw new NotFoundException($"Profession with ID {request.Id} not found");

            entity.Name = request.Name;
            entity.Code = request.Code;
            entity.LastModified = DateTimeOffset.UtcNow;
            entity.LastModifiedBy = request.LastModifiedBy;

            var success = await _professionRepository.UpdateEntity(entity);

            return Unit.Value;
        }
    }
}
