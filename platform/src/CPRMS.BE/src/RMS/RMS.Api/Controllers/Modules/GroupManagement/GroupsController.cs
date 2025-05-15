using Core.CPRMSServiceComponents.Controller;
using Core.Domain.Models.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Rms.API.Controllers.Modules.Users.AuthenController;

namespace Rms.API.Controllers.Modules.GroupManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        [HttpGet("GetGroup")]
        public async Task<IActionResult> GetName()
        {
            var response = new UserResponse
            {
                Name = "HungHPV - NhatNDA - TrieuLQ - QuyND - KhoaDD",
            };
            return Ok(response);
        }
    }
}
