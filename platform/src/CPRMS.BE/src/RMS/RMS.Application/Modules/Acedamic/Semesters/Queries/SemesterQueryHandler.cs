using Rms.Application.Modules.Acedamic.Semesters.Dtos;

namespace Rms.Application.Modules.Acedamic.Semesters.Queries
{
    public class SemesterQueryHandler
    {
        private readonly ISemesterRepository _semesterRepository;
        public SemesterQueryHandler(ISemesterRepository semesterRepository)
        {
            _semesterRepository = semesterRepository;
        }
        public async Task<IEnumerable<SemesterDto>> GetAllSemesters(CancellationToken cancellationToken = default)
        {
            var result = await _semesterRepository.GetEntities();
            var semesterDto = result.Adapt<List<SemesterDto>>();
            return semesterDto;
        }
    }
}
