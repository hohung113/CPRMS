using Microsoft.AspNetCore.Mvc;
namespace Core.CPRMSServiceComponents.Controller
{
    [ApiController]
    [Route("api/{appId:guid}/[controller]")]
    public class BaseControllerV1 : ControllerBase
    {
        protected Guid AppId => Guid.Parse((string)RouteData.Values["appId"]);
    }
}
