using Auth.API.Infrastructure.Persistence;
using Core.CPRMSServiceComponents.Controller;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auth.API.Controllers
{
    public class AuthenController : BaseControllerV1
    {
        private readonly AuthDbContext _authDbContext;
        //SemaphoreSlim - Semaphore
        public AuthenController(AuthDbContext authDbContext)     
        {
            _authDbContext = authDbContext;
        }

        [HttpGet("getusermysql")]
        public async Task<IActionResult> GetUserDemoMySQl()
        {
            
            var userMySQl = await _authDbContext.Users.ToListAsync();
            return Ok(userMySQl);
        }


        // test https://localhost:7107/authserver/123e4567-e89b-12d3-a456-426614174000/authen/getnameproject
        [HttpGet("getnameproject")]
        public IActionResult GetName()
        {
            var user = new
            {
                Name = "HungHPV", 
                Age = 18  
            };
            return Ok(user);
        }
        [HttpGet("googlelogin")]
        public IActionResult GoogleLogin()
        {
            var clientId = "75558424470-h5fg4osu2s6q7kur53fse0dq0tj3m6bc.apps.googleusercontent.com";
            //var redirectUri = "https://localhost:7093/api/user/Auth/google-callback";
            var redirectUri = "https://localhost:7092/Login?handler=GoogleCallback";
            var googleAuthUrl = $"https://accounts.google.com/o/oauth2/auth" +
                                $"?client_id={clientId}" +
                                $"&redirect_uri={redirectUri}" +
                                $"&response_type=code" +
                                $"&scope=openid%20email%20profile" +
                                $"&access_type=offline";

            return Redirect(googleAuthUrl);
        }

        [HttpGet("google-callback")]
        public async Task<IActionResult> GoogleCallback([FromQuery] string code)
        {
            if (string.IsNullOrEmpty(code))
                return BadRequest("Authorization code is missing!");
            return Ok();

        }
    }
}
