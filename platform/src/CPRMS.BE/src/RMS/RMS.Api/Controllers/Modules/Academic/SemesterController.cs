using Core.Application.Dtos;
using Rms.Application.Modules.Acedamic.Semesters.Commands.CreateSemester;
using Rms.Application.Modules.Acedamic.Semesters.Dtos;
using Rms.Application.Modules.Acedamic.Semesters.Queries;

namespace Rms.API.Controllers.Modules.Academic
{
    public class SemesterController : BaseControllerV1
    {
        private readonly ILogger<SemesterController> _logger;
        private readonly SemesterQueryHandler semesterQueryHandler;
        public SemesterController(SemesterQueryHandler semesterQueryHandler,
            ILogger<SemesterController> logger)
        {
            this.semesterQueryHandler = semesterQueryHandler;
            _logger = logger;
        }

        [HttpPost("createsemester")]
        public async Task<Guid?> CreateSemester([FromBody] SemesterDto request)
        {
            var cmd = request.Adapt<CreateSemesterCommand>();
            var result = await Dispatcher.Send(cmd);
            return result.Id;
        }
        #region For Page Return Only item and count
        [HttpGet("GetAllSemesters")]
        public async Task<BasePagedResultDto<SemesterDto>> GetAllSemesters()
        {
            var result = await semesterQueryHandler.GetAllSemesters();
            var listResults = result.ToList();
            return new BasePagedResultDto<SemesterDto>
            {
                Items = listResults,
                TotalItems = listResults.Count
            };
        } 
        #endregion


    }
}
