using MediatR;
using Rms.Application.Modules.Acedamic.Command;
using Rms.Application.Modules.Acedamic.Dto;
using Rms.Domain.Entities;
using Rms.Domain.Modules.Academic.Interface;

namespace Rms.Application.Modules.Acedamic.CommandHandler
{
    public class CreateSemesterCommandHandler : IRequestHandler<CreateSemesterCommand, SemesterDto>
    {
        private readonly ISemesterRepository _semesterRepository;
        public CreateSemesterCommandHandler(ISemesterRepository semesterRepository)
        {
            _semesterRepository = semesterRepository;
        }
        public async Task<SemesterDto> Handle(CreateSemesterCommand request, CancellationToken cancellationToken)
        {
            // using mapster herre
            var semesterEntity = request.Adapt<Semester>();
            var createdSemester = await _semesterRepository.AddEntity(semesterEntity);
            if (createdSemester == null)
            {
                throw new System.Exception("Failed to create semester in the repository.");
            }
            var semesterDto = createdSemester.Adapt<SemesterDto>();
            return await Task.FromResult(semesterDto);
        }
    }
}
