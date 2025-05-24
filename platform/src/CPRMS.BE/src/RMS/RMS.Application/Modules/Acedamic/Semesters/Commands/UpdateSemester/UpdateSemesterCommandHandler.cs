using MediatR;
using Rms.Application.Modules.Acedamic.Semesters.Commands.CreateSemester;
using Rms.Application.Modules.Acedamic.Semesters.Dtos;

namespace Rms.Application.Modules.Acedamic.Semesters.Commands.UpdateSemester
{
    public class UpdateSemesterCommandHandler : IRequestHandler<UpdateSemesterCommand, SemesterDto>
    {
        private readonly ISemesterRepository _semesterRepository;
        public UpdateSemesterCommandHandler(ISemesterRepository semesterRepository)
        {
            _semesterRepository = semesterRepository;
        }
        public async Task<SemesterDto> Handle(UpdateSemesterCommand request, CancellationToken cancellationToken)
        {
            var semesterEntity = request.Adapt<Semester>();
            var createdSemester = await _semesterRepository.UpdateEntity(semesterEntity);
            if (createdSemester == null)
            {
                throw new Exception("Failed to update semester in the repository.");
            }
            var semesterDto = createdSemester.Adapt<SemesterDto>();
            return await Task.FromResult(semesterDto);
        }
    }
}
