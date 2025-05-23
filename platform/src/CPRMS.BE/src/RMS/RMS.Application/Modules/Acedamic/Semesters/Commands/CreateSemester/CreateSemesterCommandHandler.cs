using MediatR;
using Rms.Application.Modules.Acedamic.Semesters.Dtos;


namespace Rms.Application.Modules.Acedamic.Semesters.Commands.CreateSemester
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
            var semesterEntity = request.Adapt<Semester>();
            var createdSemester = await _semesterRepository.AddEntity(semesterEntity);
            if (createdSemester == null)
            {
                throw new Exception("Failed to create semester in the repository.");
            }
            var semesterDto = createdSemester.Adapt<SemesterDto>();
            return await Task.FromResult(semesterDto);
        }
    }
}
