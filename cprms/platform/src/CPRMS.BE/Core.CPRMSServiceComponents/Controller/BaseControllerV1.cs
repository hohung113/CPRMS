using Microsoft.AspNetCore.Mvc;
namespace Core.CPRMSServiceComponents.Controller
{
    [ApiController]
    [Route("api/{appId:guid}/[controller]")]
    public class BaseControllerV1 : ControllerBase
    {
        protected Guid AppId
        {
            get
            {
                if (Guid.TryParse((string)RouteData.Values["appId"], out var id))
                    return id;
                throw new ArgumentException("Invalid appId");
            }
        }

    }
}