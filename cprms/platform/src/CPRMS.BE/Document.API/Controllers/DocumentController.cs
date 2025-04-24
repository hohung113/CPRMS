using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Document.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        ILogger<DocumentController> _logger;
        public DocumentController(ILogger<DocumentController> logger)
        {
            _logger = logger;
        }
    }
}
