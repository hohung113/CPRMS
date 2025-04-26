using Core.CPRMSServiceComponents.Controller;
using Core.Customized.CustomizePoints.DocumentCentrePoints.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Document.API.Controllers
{
    public class DocumentController : BaseControllerV1
    {
        ILogger<DocumentController> _logger;
        private readonly IAuthorizationInterfaceClient _authApi;
        public DocumentController(ILogger<DocumentController> logger, IAuthorizationInterfaceClient authorizationInterfaceClient)
        {
            _logger = logger;
            _authApi = authorizationInterfaceClient;
        }

        [HttpGet("userinfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            // demo using refit https://localhost:7107/rsm/documentserver/3f2504e0-4f89-11d3-9a0c-0305e82c3301/document/userinfo 
            // refit to call between authservice and documentservice
            var result = await _authApi.GetUserAsync();
            result.Age = 22;
            result.Name = "Karim Ho test refit";
            return Ok(result);
        }
    }
}
