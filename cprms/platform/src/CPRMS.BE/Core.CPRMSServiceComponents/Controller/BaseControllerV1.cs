using Core.Api.MediatRCustom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.Api.Controller
{
    //[Route("api/{appId:guid}/[controller]")]
    [ApiController]
    //[Authorize]
    [Route("api/v1/[controller]")]
    public class BaseControllerV1 : ControllerBase
    {
    }
}