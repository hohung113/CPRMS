using Auth.API.Infrastructure.Persistence;
using Core.Application.ServiceModel;
using Core.CPRMSServiceComponents.Controller;
using Core.CPRMSServiceComponents.ServiceComponents.JWTService;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly ILogger<AuthenController> _logger;
        private readonly AuthDbContext _authDbContext;
        private readonly AccountSettings _accountSettings;
        private readonly TokenService _tokenService;
        public AuthenController(
            ILogger<AuthenController> logger, 
            IOptions<AccountSettings> options,
            AuthDbContext authDbContext, 
            TokenService tokenService
            )
        {
            _logger = logger;
            _authDbContext = authDbContext;
            _accountSettings = options.Value;
            _tokenService = tokenService;
        }
        private string AppId => RouteData?.Values["appId"]?.ToString();
    
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

        [HttpGet("google-login")]
        public IActionResult GoogleLogin()
        {
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = $"/api/{AppId}/authen/google-callback"
            }, GoogleDefaults.AuthenticationScheme);
        }
        [HttpGet("google-callback")]
        public async Task<IActionResult> GoogleCallback()
        {
            //var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var authResult = await HttpContext.AuthenticateAsync();
            if (!authResult.Succeeded)
            {
                _logger.LogWarning("Google authentication callback failed or principal not found.");
                return Unauthorized("Authentication failed.");
            }
            var claims = authResult.Principal.Claims;
            var principal = authResult.Principal;
            var email = principal.FindFirstValue(ClaimTypes.Email);
            var googleId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var givenName = principal.FindFirstValue(ClaimTypes.GivenName);
            var surname = principal.FindFirstValue(ClaimTypes.Surname);
           
            // Get Image Profile
            var accessToken = authResult.Properties.GetTokenValue("access_token");
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync("https://www.googleapis.com/oauth2/v2/userinfo");

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Cannot retrieve user info from Google.");
            }
            // Find User In Db
            //var user = await _userService.FindOrCreateUserAsync(email, googleId, givenName, surname);
            //if (user == null)
            //{
            //    _logger.LogError("Failed to find or create user for email: {Email}", email);
            //    return StatusCode(500, new { Message = "An error occurred while processing your account." });
            //}
            var json = await response.Content.ReadAsStringAsync();
            var googleUser = JsonSerializer.Deserialize<GoogleUserInfo>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Tạo JWT như cũ
            var user = await _authDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            var (token, accessTokenExpiresAt) = _tokenService.GenerateAccessToken(user);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _logger.LogInformation("Access token generated for user: {Email}", user.Email);

            return Ok(new
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExpiresAt,
                User = new { user.Id, user.Email, user.FullName, user.Campus }
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
