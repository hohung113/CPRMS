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

        [HttpPost("create")]
        [ValidatedBy<CreateSemesterValidator>]
        public async Task<BaseResponse<Guid?>> CreateSemester([FromBody] SemesterDto request)
        {
            var cmd = request.Adapt<CreateSemesterCommand>();
            return await this.Run(_logger, async () =>
            {
                var result = await Dispatcher.Send(cmd);
                return result.Id;
            });
        }
        [HttpPost("update")] 
        //[HttpPut("update")]
        public async Task<BaseResponse<Guid?>> UpdateSemester([FromBody] SemesterDto request)
        {
            var cmd = request.Adapt<UpdateSemesterCommand>();
            return await this.Run(_logger, async () =>
            {
                var result = await Dispatcher.Send(cmd);
                return result.Id; 
            });
        }
        [HttpDelete("delete")]
        public async Task<BaseResponse<Guid?>> DeleteSemester([FromBody] SemesterDto request)
        {
            var cmd = request.Adapt<DeleteSemesterCommand>();
            return await this.Run(_logger, async () =>
            {
                var result = await Dispatcher.Send(cmd);
                return result.Id;
            });
        }
        [Authorize(Roles =CprmsRoles.Admin)]
        [HttpGet("getall")]
        public async Task<BaseResponse<IEnumerable<SemesterDto>>> GetAllSemester()
        {
            var result = semesterQueryHandler.GetAllSemesters();
            return await this.Run(_logger, () => result);
        }

        //[HttpGet("paginationsemester")]
        //public async Task<BaseResponse<IEnumerable<SemesterDto>>> GetAllSemester([FromBody]PagedRequest<> )
        //{
        //    var result = semesterQueryHandler.GetAllSemesters();
        //    return await this.Run(_logger, () => result);
        //}
        [HttpGet("{id}")]
        public async Task<BaseResponse<SemesterDto>> GetSemesterById(Guid id)
        {
            return await this.Run(_logger, () => semesterQueryHandler.GetSemester(id));
        }
    }
}


// ---> reference
//public async Task<BaseResponse<List<SemesterDto>>> GetAll()
//{
//    return await this.Run(_logger, async () =>
//    {
//        var semesters = await _semesterService.GetAllSemesters();
//        return semesters;
//    });
//}
//[HttpGet("{id}")]
//public async Task<BaseResponse<SemesterDto>> GetById(Guid id)
//{
//    return await this.Run(_logger, async () =>
//    {
//        var semester = await _semesterService.GetSemesterById(id);
//        return semester;
//    });
//}
//[HttpPost]
//public async Task<BaseResponse<Guid>> Create([FromBody] SemesterDto newSemester)
//{
//    return await this.Run(_logger, async () =>
//    {
//        var newId = await _semesterService.CreateSemester(newSemester);
//        return newId;
//    });
//}
//[HttpPut("{id}")]
//public async Task<BaseResponse<bool>> Update(Guid id, [FromBody] SemesterDto updateSemester)
//{
//    return await this.Run(_logger, async () =>
//    {
//        var result = await _semesterService.UpdateSemester(id, updateSemester);
//        return result;
//    });
//}
//[HttpDelete("{id}")]
//public async Task<BaseResponse<bool>> Delete(Guid id)
//{
//    return await this.Run(_logger, async () =>
//    {
//        var result = await _semesterService.DeleteSemester(id);
//        return result;
//    });
//}
//[HttpGet("GetAllSemesters")]
//public async Task<BasePagedResultDto<SemesterDto>> GetAllSemesters()
//{
//    var result = await semesterQueryHandler.GetAllSemesters();
//    var listResults = result.ToList();
//    return new BasePagedResultDto<SemesterDto>
//    {
//        Items = listResults,
//        TotalItems = listResults.Count
//    };
//}
