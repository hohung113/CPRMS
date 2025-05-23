using Microsoft.IdentityModel.Tokens;
using Rms.Domain.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Rms.API.Middlewares
{
    public class JwtAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ILogger<JwtAuthenticationMiddleware> _logger;

        public JwtAuthenticationMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<JwtAuthenticationMiddleware> logger)
        {
            _next = next;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                CPRMSHttpContext.Clear();
                var token = ExtractTokenFromHeader(context);
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
            finally
            {
                CPRMSHttpContext.Clear();
            }
        }

        private string? ExtractTokenFromHeader(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader))
                return null;

            if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                return authHeader.Substring("Bearer ".Length).Trim();
            }

            return null;
        }

        private async Task AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var jwtKey = _configuration["JWT:Key"];
                if (string.IsNullOrEmpty(jwtKey))
                {
                    _logger.LogError("JWT Key is not configured");
                    return;
                }

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    RequireExpirationTime = true
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    if (!jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    {
                        _logger.LogWarning("Invalid token algorithm: {Algorithm}", jwtSecurityToken.Header.Alg);
                        return;
                    }
                }

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