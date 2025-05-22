using Core.Api.MediatRCustom;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Api.Controller
{
    //[Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BaseControllerV1 : ControllerBase
    {
        private IDispatcher _dispatcher;
        protected IDispatcher Dispatcher => _dispatcher ??= HttpContext.RequestServices.GetRequiredService<IDispatcher>();
        //protected string CurrentUserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}