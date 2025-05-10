using Auth.API.Infrastructure.Persistence;
using Core.Application.ServiceModel;
using Core.CPRMSServiceComponents.Controller;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Auth.API.Controllers
{
    public class AuthenController : BaseControllerV1
    {
        private readonly AuthDbContext _authDbContext;
        private readonly ILogger<AuthenController> _logger;
        private readonly AccountSettings _accountSettings;
        public AuthenController(ILogger<AuthenController> logger, AuthDbContext authDbContext, IOptions<AccountSettings> options)
        {
            _logger = logger;
            _authDbContext = authDbContext;
            _accountSettings = options.Value;
        }
        private string AppId => RouteData?.Values["appId"]?.ToString();
     
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
            // test
            var accessToken = authResult.Properties.GetTokenValue("access_token");
            var claims = authResult.Principal.Claims;
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value
                     ?? claims.FirstOrDefault(c => c.Type == "email")?.Value
                     ?? claims.FirstOrDefault(c => c.Type == "urn:google:email")?.Value;
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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
        public class GoogleUserInfo
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public string Name { get; set; }
            public string Picture { get; set; } 
        }

    }
}
