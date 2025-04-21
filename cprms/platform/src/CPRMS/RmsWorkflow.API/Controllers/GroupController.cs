using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RmsWorkflow.API.Controllers
{
    [Route("api/{tenantId}/[controller]")] 
    [ApiController]
    public class GroupController : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateGroup()
        {
            return Ok();
        }
    }
}
