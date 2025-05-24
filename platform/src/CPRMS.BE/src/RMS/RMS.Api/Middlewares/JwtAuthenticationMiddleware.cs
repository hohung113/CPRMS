using Microsoft.IdentityModel.Tokens;
using Rms.Application.Services;
using Rms.Domain.Context;
using System.Security.Claims;

namespace Rms.API.Middlewares
{
    public class JwtAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly TokenService tokenService;
        private readonly ILogger<JwtAuthenticationMiddleware> _logger;

        public JwtAuthenticationMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<JwtAuthenticationMiddleware> logger, TokenService tokenService)
        {
            _next = next;
            _configuration = configuration;
            _logger = logger;
            this.tokenService = tokenService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                CPRMSHttpContext.Clear();
                var token = tokenService.ExtractTokenFromHeader(context);
                if (!string.IsNullOrEmpty(token))
                {
                    await AttachUserToContext(context, token);
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in JWT Authentication Middleware");
                await _next(context);
            }
        }

        private async Task AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var principal = tokenService.ValidateToken(token);
                // Set user context
                var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var userName = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                var roleName = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                CPRMSHttpContext.SetCurrentUser(userId, userName, roleName);

                _logger.LogInformation("User authenticated: {UserId}, {UserName}, {Role}", userId, userName, roleName);
            }
            catch (SecurityTokenExpiredException ex)
            {
                _logger.LogWarning("Token expired: {Message}", ex.Message);
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token expired");
                return;
            }
            catch (SecurityTokenException ex)
            {
                _logger.LogWarning("Invalid token: {Message}", ex.Message);
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid token");
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating token");
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Authentication failed");
                return;
            }
        }
    }
}