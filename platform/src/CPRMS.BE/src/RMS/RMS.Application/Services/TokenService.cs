using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Rms.Application.Services
{
    public class TokenService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<TokenService> _logger;
        public TokenService(IConfiguration config, ILogger<TokenService> logger)
        {
            _logger = logger;
            _config = config;
        }
        public TokenResponse GenerateJwtToken(GoogleLoginResponseDto user)
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
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(JwtRegisteredClaimNames.Sub, user.Email),
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

            var issuedAt = DateTime.UtcNow;
            var expiresAt = issuedAt.AddHours(expiresInHours);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                notBefore: issuedAt,
                expires: expiresAt,
                signingCredentials: creds
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return new TokenResponse
            {
                AccessToken = accessToken,
                TokenType = "Bearer",
                ExpiresIn = (int)(expiresAt - issuedAt).TotalSeconds,
                IssuedAt = issuedAt
            };
        }

        public string? ExtractTokenFromHeader(HttpContext context)
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

        public ClaimsPrincipal? ValidateToken(string token) {
            var jwtKey = _config["JWT:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new Exception("Jwt key not provied");
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
                    throw new Exception("Invalid token algorithm");
                }
            }
            return principal;
        }

    }
}
