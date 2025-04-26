using Core.CPRMSServiceComponents.Controller;
using Core.Customized.CustomizePoints.DocumentCentrePoints.Interface;
using Core.Domain.Models.Base;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public async Task<BaseResponse<UserResponse>> GetUserInfo()
        {
            // demo using refit https://localhost:7107/rsm/documentserver/3f2504e0-4f89-11d3-9a0c-0305e82c3301/document/userinfo 
            // refit to call between authservice and documentservice
            // var result = await _authApi.GetUserAsync();

            var response = new UserResponse
            {
                Age = 22,
                Name = "Karim Ho test refit"
            };
            return await this.Run<UserResponse>(_logger, async () => response);
        }
        // Test UserInforResponse
    }
    public class UserResponse
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
