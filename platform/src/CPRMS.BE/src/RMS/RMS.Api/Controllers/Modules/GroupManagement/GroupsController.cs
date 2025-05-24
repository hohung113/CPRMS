namespace Rms.API.Controllers.Modules.GroupManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        [HttpGet("GetGroup")]
        public async Task<IActionResult> GetName()
        {
            return Ok();
        }
    }
}
