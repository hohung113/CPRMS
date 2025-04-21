using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RmsWorkflow.API.Controllers
{
    [Route("api/{tenantId}/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateComment()
        {
            return Ok();
        }
    }
}
