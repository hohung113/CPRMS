using MediatR;
using Rms.Application.Modules.Acedamic.Semesters.Dtos;

namespace Rms.Application.Modules.Acedamic.Semesters.Commands.DeleteSemester
{
    public class DeleteSemesterCommandHandler : IRequestHandler<DeleteSemesterCommand, SemesterDto>
    {
        private readonly ISemesterRepository _semesterRepository;
        public DeleteSemesterCommandHandler(ISemesterRepository semesterRepository)
        {
            _semesterRepository = semesterRepository;
        }
        public async Task<SemesterDto> Handle(DeleteSemesterCommand request, CancellationToken cancellationToken)
        {
            var semesterEntity = request.Adapt<Semester>();
            var createdSemester = await _semesterRepository.DeleteEntity(semesterEntity);
            if (createdSemester == null)
            {
                throw new Exception("Failed to create semester in the repository.");
            }
            var semesterDto = createdSemester.Adapt<SemesterDto>();
            return await Task.FromResult(semesterDto);
        }
    }
}
