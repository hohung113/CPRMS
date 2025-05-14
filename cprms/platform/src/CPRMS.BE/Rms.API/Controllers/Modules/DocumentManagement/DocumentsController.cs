using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Rms.API.Controllers.Modules.DocumentManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        [HttpGet("GetDocument")]
        public async Task<IActionResult> GetName()
        {
            return Ok();
        }
    }
}
