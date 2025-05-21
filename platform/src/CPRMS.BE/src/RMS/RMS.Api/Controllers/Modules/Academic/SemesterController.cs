using Microsoft.AspNetCore.Authorization;
using Rms.Application.Modules.Acedamic.Command;
using Rms.Application.Modules.Acedamic.QueryHandler;

namespace Rms.API.Controllers.Modules.Academic
{
    public class SemesterController : BaseControllerV1
    {
        private readonly SemesterQueryHandler semesterQueryHandler;
        public SemesterController(SemesterQueryHandler semesterQueryHandler)
        {
            this.semesterQueryHandler = semesterQueryHandler;
        }
        ////[Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateSemester([FromBody] CreateSemesterCommand command)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            var result = await Dispatcher.Send(command);
            return Ok(result);
            //return CreatedAtAction(nameof(GetSemesterById), new { id = result.Id }, result); // Example, you'd need a GetSemesterById endpoint
        }
        [HttpGet("GetAllSemesters")]
        public async Task<IActionResult> GetAllSemesters()
        {
            var result = await semesterQueryHandler.GetAllSemesters();
            return Ok(result);
        }
    }
}
