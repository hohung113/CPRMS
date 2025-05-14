using Core.CPRMSServiceComponents.Controller;
using Core.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;
using Rms.Application.Modules.UserManagement.Command;
using Rms.Infrastructure.Persistence;

namespace Rms.API.Controllers.Modules.Users
{
    public class AuthenController : BaseControllerV1
    {
        private readonly ILogger<AuthenController> _logger;
        private readonly RmsDbContext _rmsDbContext;
        private readonly IMediator _mediator;
        public AuthenController(
            ILogger<AuthenController> logger,
            RmsDbContext rmsDbContext,
            IMediator mediator
            )
        {
            _rmsDbContext = rmsDbContext;
            _logger = logger;
            _mediator = mediator;
        }

        private string AppId => RouteData?.Values["appId"]?.ToString();

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginCommand command)
        //{
        //    var isSuccess = await _mediator.Send(command);
        //    if (isSuccess)
        //        return Ok(new { Message = "Login successful" });
        //    return Unauthorized(new { Message = "Invalid email or password" });
        //}
        [HttpGet("getmemberofprojectCPRMS")]
        public async Task<BaseResponse<UserResponse>> GetName()
        {
            var response = new UserResponse
            {
                Name = "HungHPV - NhatNDA - TrieuLQ - QuyND - KhoaDD",
            };
            return await this.Run<UserResponse>(_logger, async () => response);
        }
        [HttpGet("GetRoleName")]
        public async Task<IActionResult> GetRoleName()
        {
            var roleName = await _rmsDbContext.Roles.Where(x => x.IsDeleted == false).Select(x => x.RoleName).ToListAsync();
            return Ok(roleName);
        }
        public class UserResponse
        {
            public string Name { get; set; }
        }
    }
}
//[HttpPost("login")]
//public async Task<IActionResult> Login([FromBody] LoginWithEmailCommand command)
//{
//    var result = await _mediator.Send(command);
//    if (!result.Success)
//    {
//        return Unauthorized(new { result.Message });
//    }
//    return Ok(result);
//}


//[HttpGet("google-login")]
//public IActionResult GoogleLogin()
//{
//    return Challenge(new AuthenticationProperties
//    {
//        RedirectUri = $"/api/{AppId}/authen/google-callback"
//    }, GoogleDefaults.AuthenticationScheme);
//}
//[HttpGet("google-callback")]
//public async Task<IActionResult> GoogleCallback()
//{
//    //var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
//    var authResult = await HttpContext.AuthenticateAsync();
//    if (!authResult.Succeeded)
//    {
//        _logger.LogWarning("Google authentication callback failed or principal not found.");
//        return Unauthorized("Authentication failed.");
//    }
//    var claims = authResult.Principal.Claims;
//    var principal = authResult.Principal;
//    var email = principal.FindFirstValue(ClaimTypes.Email);
//    var googleId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
//    var givenName = principal.FindFirstValue(ClaimTypes.GivenName);
//    var surname = principal.FindFirstValue(ClaimTypes.Surname);

//    // Get Image Profile
//    var accessToken = authResult.Properties.GetTokenValue("access_token");
//    using var client = new HttpClient();
//    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
//    var response = await client.GetAsync("https://www.googleapis.com/oauth2/v2/userinfo");

//    if (!response.IsSuccessStatusCode)
//    {
//        return BadRequest("Cannot retrieve user info from Google.");
//    }
//    // Find User In Db
//    //var user = await _userService.FindOrCreateUserAsync(email, googleId, givenName, surname);
//    //if (user == null)
//    //{
//    //    _logger.LogError("Failed to find or create user for email: {Email}", email);
//    //    return StatusCode(500, new { Message = "An error occurred while processing your account." });
//    //}
//    var json = await response.Content.ReadAsStringAsync();
//    var googleUser = JsonSerializer.Deserialize<GoogleUserInfo>(json, new JsonSerializerOptions
//    {
//        PropertyNameCaseInsensitive = true
//    });

//    // Tạo JWT như cũ
//    var user = await _authDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
//    var (token, accessTokenExpiresAt) = _tokenService.GenerateAccessToken(user);
//    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
//    _logger.LogInformation("Access token generated for user: {Email}", user.Email);

//    return Ok(new
//    {
//        AccessToken = token,
//        AccessTokenExpiration = accessTokenExpiresAt,
//        User = new { user.Id, user.Email, user.FullName, user.Campus }
//    });
//}

//[HttpGet("user")]
//public IActionResult GetUser()
//{
//    if (User.Identity.IsAuthenticated)
//    {
//        var userInfo = new
//        {
//            Name = User.FindFirst(ClaimTypes.Name)?.Value,
//            Email = User.FindFirst(ClaimTypes.Email)?.Value,
//            Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
//        };
//        return Ok(userInfo);
//    }
//    return Unauthorized("User not authenticated.");
//}
//private string GenerateJwtToken(ClaimsPrincipal user)
//{
//    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secure-key-here"));
//    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//    var token = new JwtSecurityToken(
//        issuer: "your-issuer",
//        audience: "your-audience",
//        claims: user.Claims,
//        expires: DateTime.Now.AddHours(1),
//        signingCredentials: creds);

//    return new JwtSecurityTokenHandler().WriteToken(token);
//}
//public class GoogleUserInfo
//{
//    public string Id { get; set; }
//    public string Email { get; set; }
//    public string Name { get; set; }
//    public string Picture { get; set; }
//}