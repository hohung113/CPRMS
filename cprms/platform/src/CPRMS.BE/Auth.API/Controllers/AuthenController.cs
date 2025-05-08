using Auth.API.Infrastructure.Persistence;
using Core.CPRMSServiceComponents.Controller;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Auth.API.Controllers
{
    public class AuthenController : BaseControllerV1
    {
        ILogger<AuthenController> _logger;
        public AuthenController(ILogger<AuthenController> logger)
        {
            _logger = logger;
        }
        private string AppId => RouteData?.Values["appId"]?.ToString();
        // private readonly AuthDbContext _authDbContext;
        //SemaphoreSlim - Semaphore

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

        [HttpGet("login")]
        public IActionResult Login()
        {
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = $"/api/{AppId}/authen/callback"
            }, GoogleDefaults.AuthenticationScheme);
        }
        [HttpGet("callback")]
        public async Task<IActionResult> Callback()
        {
            var authResult = await HttpContext.AuthenticateAsync();
            if (!authResult.Succeeded)
            {
                return Unauthorized("Authentication failed.");
            }

            var accessToken = authResult.Properties.GetTokenValue("access_token");

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Gọi Google UserInfo endpoint để lấy thông tin chi tiết (có avatar)
            var response = await client.GetAsync("https://www.googleapis.com/oauth2/v2/userinfo");

            if (!response.IsSuccessStatusCode)
                return BadRequest("Cannot retrieve user info from Google.");

            var json = await response.Content.ReadAsStringAsync();
            var googleUser = JsonSerializer.Deserialize<GoogleUserInfo>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Tạo JWT như cũ
            var token = GenerateJwtToken(authResult.Principal);

            return Ok(new
            {
                Token = token,
                User = new
                {
                    Name = googleUser.Name,
                    Email = googleUser.Email,
                    Id = googleUser.Id,
                    Picture = googleUser.Picture
                }
            });
        }

        [HttpGet("user")]
        public IActionResult GetUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userInfo = new
                {
                    Name = User.FindFirst(ClaimTypes.Name)?.Value,
                    Email = User.FindFirst(ClaimTypes.Email)?.Value,
                    Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                };
                return Ok(userInfo);
            }
            return Unauthorized("User not authenticated.");
        }
        private string GenerateJwtToken(ClaimsPrincipal user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secure-key-here"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "your-issuer",
                audience: "your-audience",
                claims: user.Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //[HttpGet("getusermysql")]
        //public async Task<IActionResult> GetUserDemoMySQl()
        //{
            
        //    var userMySQl = await _authDbContext.Users.ToListAsync();
        //    return Ok(userMySQl);
        //}

        //[HttpGet("googlelogin")]
        //public IActionResult GoogleLogin()
        //{
        //    var clientId = "75558424470-h5fg4osu2s6q7kur53fse0dq0tj3m6bc.apps.googleusercontent.com";
        //    //var redirectUri = "https://localhost:7093/api/user/Auth/google-callback";
        //    var redirectUri = "https://localhost:7092/Login?handler=GoogleCallback";
        //    var googleAuthUrl = $"https://accounts.google.com/o/oauth2/auth" +
        //                        $"?client_id={clientId}" +
        //                        $"&redirect_uri={redirectUri}" +
        //                        $"&response_type=code" +
        //                        $"&scope=openid%20email%20profile" +
        //                        $"&access_type=offline";

        //    return Redirect(googleAuthUrl);
        //}

        //[HttpGet("google-callback")]
        //public async Task<IActionResult> GoogleCallback([FromQuery] string code)
        //{
        //    if (string.IsNullOrEmpty(code))
        //        return BadRequest("Authorization code is missing!");
        //    return Ok();

        //}
        public class GoogleUserInfo
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public string Name { get; set; }
            public string Picture { get; set; } 
        }

    }
}
