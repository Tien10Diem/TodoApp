
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Domain.Entities;
using System.Security.Claims;

namespace Infrastructure.Helper
{
    public static class JwtHelper
    {
        public static string GenerateToken(User user, IConfiguration config, int expiresMinutes = 1)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            
            var secret = config["Jwt:Key"] ?? throw new InvalidOperationException("Missing Jwt:Key");

            var jti = Guid.NewGuid().ToString();

            var claims = new[]
            {
                // dùng ClaimTypes để ASP.NET map User.Identity.Name, NameIdentifier tự động
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()), // ID
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),    // Name
                new Claim(JwtRegisteredClaimNames.Jti, jti),
            };

            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
