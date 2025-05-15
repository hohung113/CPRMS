using Core.Application.ServiceModel;
using Core.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Core.Api.ServiceComponents.JWTService
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public (string Token, DateTime ExpiresAt) GenerateAccessToken(User user)
        {
            var jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured.");
            var issuer = _configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer not configured.");
            var audience = _configuration["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience not configured.");
            // Lấy thời gian hết hạn từ cấu hình, mặc định là 60 phút nếu không có
            var accessTokenExpirationMinutes = _configuration.GetValue<int>("Jwt:AccessTokenExpirationMinutes", 60);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
                new Claim("uid", user.Id.ToString())
            };

            if (!string.IsNullOrEmpty(user.FullName))
            {
                claims.Add(new Claim(ClaimTypes.Name, user.FullName));
            }
           
            // foreach (var role in user.Roles) { claims.Add(new Claim(ClaimTypes.Role, role)); }
            var expiresAt = DateTime.UtcNow.AddMinutes(accessTokenExpirationMinutes);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiresAt,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return (tokenHandler.WriteToken(token), expiresAt);
        }
    }
}