using Rms.Application.Modules.UserManagement.Command;

namespace Rms.API.Controllers.Modules.Users
{
    public class UserController : BaseControllerV1
    {
        [Authorize(Roles = CprmsRoles.Admin)]
        [HttpPost("createuser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await Dispatcher.Send(command);
            return Ok(result);
        }
        //[HttpPost("importusers")]
    }
}
