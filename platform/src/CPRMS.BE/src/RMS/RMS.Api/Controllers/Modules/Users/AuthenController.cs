using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Rms.Application.Modules.UserManagement.Command;
using Rms.Application.Modules.UserManagement.Dto;
using Rms.Application.Modules.UserManagement.QueryHandler;
using Rms.Domain.Constants;
using Rms.Domain.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;

namespace Rms.API.Controllers.Modules.Users
{
    public class AuthenController : BaseControllerV1 
    {
        private readonly UserSystemQueryHandler _queryHandler;
        private readonly ILogger<AuthenController> _logger;
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;
        private readonly RmsDbContext _rmsDbContext;
        private readonly AccountSettings _accountSettings;
        private readonly RmsSystemConfig _rmsSystemConfig;

        public AuthenController(
            ILogger<AuthenController> logger,
            RmsDbContext rmsDbContext,
            IMediator mediator,
            IOptions<AccountSettings> options,
            IOptions<RmsSystemConfig> rmsSystemConfig,
            UserSystemQueryHandler queryHandler,
            IConfiguration config
            )
        {
            _rmsDbContext = rmsDbContext;
            _logger = logger;
            _mediator = mediator;
            _accountSettings = options.Value;
            _rmsSystemConfig = rmsSystemConfig.Value;
            _config = config;
            _queryHandler = queryHandler;
        }

        [HttpGet("google-login")]
        public IActionResult GoogleLogin()
        {
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = _rmsSystemConfig.OAuthSettings.GoogleCallbackUrl,
            }, GoogleDefaults.AuthenticationScheme);
        }


        [HttpGet("google-callback")]
        public async Task<IActionResult> GoogleCallback()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!authenticateResult.Succeeded || authenticateResult.Principal == null)
            {
                return BadRequest("Validate when Google fails.");
            }
            var principal = authenticateResult.Principal;
            var email = principal.FindFirstValue(ClaimTypes.Email);
            // check in system , account admin , account user 
            GetGoogleUserDetailsQuery requestGmail = new GetGoogleUserDetailsQuery
            {
                Email = email,
            };
            var userSystemInfor = await _queryHandler.GetUserSystemByEmail(requestGmail);
            if (userSystemInfor == null)
            {
                return Unauthorized("User does not exist in the system.");
            }
            try
            {
                if (userSystemInfor.Email == _accountSettings.Admin.Email) {
                   // Create Admin Account
                    CreateUserCommand command = new CreateUserCommand
                    {
                        Code = CprmsConstants.CprmsAdmin,
                        Email = _accountSettings.Admin.Email,
                        FullName = CprmsConstants.CprmsAdminDisplayName,
                    };
                    var result = await Dispatcher.Send(command);
                }
                var token = await GenerateJwtToken(userSystemInfor);
                return Ok(new { token });
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest("Invalid user to generate token.");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred during token generationn.");
            }
        }
        private async Task<string> GenerateJwtToken(GoogleLoginResponseDto user)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var keyString = jwtSettings["Key"];
            if (string.IsNullOrEmpty(keyString))
            {
                throw new InvalidOperationException("JWT Key not configured.");
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                //new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            if (user.RoleNames != null)
            {
                foreach (var roleName in user.RoleNames)
                {
                    if (!string.IsNullOrEmpty(roleName))
                    {
                        claims.Add(new Claim(ClaimTypes.Role, roleName));
                    }
                }
            }
            double expiresInHours;
            if (!double.TryParse(jwtSettings["ExpiresInHours"], out expiresInHours))
            {
                expiresInHours = 2;
            }

            var tokenDescriptor = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(expiresInHours),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        [Authorize(Roles = "Student")]
        [HttpGet("getmemberofprojectCPRMS")]
        public async Task<BaseResponse<UserResponse>> GetName()
        {
            var emailAdmin = _accountSettings.Admin.Email;
            var response = new UserResponse
            {
                Name = "HungHPV - NhatNDA - TrieuLQ - QuyND - KhoaDD",
            };
            return await this.Run<UserResponse>(_logger, async () => response);
        }
        //[Authorize(Roles ="Admin")]
        [HttpGet("GetRoleName")]
        public async Task<IActionResult> GetRoleName()
        {
            _logger.LogInformation("Controller UserId: {UserId}, UserName: {UserName}", CPRMSHttpContext.UserId, CPRMSHttpContext.UserName);
            var userId = CPRMSHttpContext.UserId;
            var userName = CPRMSHttpContext.UserName;
            var roleName = await _rmsDbContext.Roles.Where(x => x.IsDeleted == false).Select(x => x.RoleName).ToListAsync();
            return Ok(roleName);
        }
        public class UserResponse
        {
            public string Name { get; set; }
        }
    }
}
//var authResult = await HttpContext.AuthenticateAsync();
//var accessToken = authResult.Properties.GetTokenValue("access_token");
//var claims = authResult.Principal.Claims;
//var principal = authResult.Principal;
//var email = principal.FindFirstValue(ClaimTypes.Email);

//using var client = new HttpClient();
//client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
//var response = await client.GetAsync("https://www.googleapis.com/oauth2/v2/userinfo");
//var json = await response.Content.ReadAsStringAsync();