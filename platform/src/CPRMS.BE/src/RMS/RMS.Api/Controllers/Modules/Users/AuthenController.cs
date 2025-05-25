using Core.Utility.Enums;
using Microsoft.AspNetCore.Authentication.Cookies;
using Rms.Application.Modules.UserManagement.Command;
using Rms.Application.Modules.UserManagement.Dto;
using Rms.Application.Modules.UserManagement.QueryHandler;
using Rms.Application.Services;
using Rms.Domain.Context;
using System.Security.Claims;

namespace Rms.API.Controllers.Modules.Users
{
    public class AuthenController : BaseControllerV1
    {
        private readonly UserSystemQueryHandler _queryHandler;
        private readonly ILogger<AuthenController> _logger;
        private readonly RmsDbContext _rmsDbContext;
        private readonly AccountSettings _accountSettings;
        private readonly RmsSystemConfig _rmsSystemConfig;
        private readonly TokenService _tokenService;
        public AuthenController(
            ILogger<AuthenController> logger,
            RmsDbContext rmsDbContext,
            IMediator mediator,
            IOptions<AccountSettings> options,
            IOptions<RmsSystemConfig> rmsSystemConfig,
            UserSystemQueryHandler queryHandler,
            TokenService tokenService
            )
        {
            _rmsDbContext = rmsDbContext;
            _logger = logger;
            _accountSettings = options.Value;
            _rmsSystemConfig = rmsSystemConfig.Value;
            _queryHandler = queryHandler;
            _tokenService = tokenService;
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
        public async Task<BaseResponse<TokenResponse>> GoogleCallback()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!authenticateResult.Succeeded || authenticateResult.Principal == null)
            {
                return new BaseResponse<TokenResponse>
                {
                    State = ResponseState.Error,
                    ErrorCode = ErrorCode.Unauthorized,
                    Message = "Google authentication failed.",
                    Result = null
                };
            }

            var principal = authenticateResult.Principal;
            var email = principal.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(email))
            {
                return new BaseResponse<TokenResponse>
                {
                    State = ResponseState.Error,
                    ErrorCode = ErrorCode.InvalidRequest,
                    Message = "Email not found in Google account.",
                    Result = null
                };
            }

            var requestGmail = new GetGoogleUserDetailsQuery { Email = email };
            var userSystemInfor = await _queryHandler.GetUserSystemByEmail(requestGmail);

            if (userSystemInfor == null)
            {
                return new BaseResponse<TokenResponse>
                {
                    State = ResponseState.Error,
                    ErrorCode = ErrorCode.NotFound,
                    Message = "User does not exist in the system.",
                    Result = null
                };
            }
            if (userSystemInfor.Email == _accountSettings.Admin.Email)
            {
                var command = new CreateUserCommand
                {
                    Code = CprmsConstants.CprmsAdmin,
                    Email = _accountSettings.Admin.Email,
                    FullName = CprmsConstants.CprmsAdminDisplayName,
                };
                await Dispatcher.Send(command);
            }

            var token = _tokenService.GenerateJwtToken(userSystemInfor);

            return new BaseResponse<TokenResponse>
            {
                State = ResponseState.Ok,
                ErrorCode = ErrorCode.None,
                Message = "Token generated successfully.",
                Result = token,
                HasPermission = true
            };
        }

        [Authorize(Roles =CprmsRoles.Student)]
        [HttpGet("GetRoleName")]
        public async Task<IActionResult> GetRoleName()
        {
            _logger.LogInformation("Controller UserId: {UserId}, UserName: {UserName}", CPRMSHttpContext.UserId, CPRMSHttpContext.UserName);
            var userId = CPRMSHttpContext.UserId;
            var userName = CPRMSHttpContext.UserName;
            var roleName = await _rmsDbContext.Roles.Where(x => x.IsDeleted == false).Select(x => x.RoleName).ToListAsync();
            return Ok(roleName);
        }
    }
}