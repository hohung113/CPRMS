using Rms.Domain.Modules.Academic.Interface;
using Core.Api.MediatRCustom;
using MediatR;
using Core.Utility.Exceptions;

namespace Rms.Application.Modules.Acedamic.Profession.Commands.DeleteProfession
{
    public class DeleteProfessionCommandHandler : IRequestHandler<DeleteProfessionCommand, Unit>
    {
        private readonly IProfessionRepository _professionRepository;

        public DeleteProfessionCommandHandler(IProfessionRepository professionRepository)
        {
            _professionRepository = professionRepository;
        }

        public async Task<Unit> Handle(DeleteProfessionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _professionRepository.GetEntity(request.Id);
            if (entity == null)
                throw new NotFoundException($"Profession with ID {request.Id} not found");

            var success = await _professionRepository.DeleteEntity(entity);

            return Unit.Value;
        }
    }

}
