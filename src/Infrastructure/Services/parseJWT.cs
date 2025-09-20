// src/YourApp.Infrastructure/Security/JwtHelper.cs
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Services
{
    public static class parseJWT
    {
        public static ClaimsPrincipal ValidateTokenAndGetPrincipal(string token, IConfiguration config)
        {
            if (string.IsNullOrEmpty(token)) throw new ArgumentNullException(nameof(token));

            var secret = config["Jwt:key"];
            if (string.IsNullOrWhiteSpace(secret)) throw new InvalidDataException("Jwt missing");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(config["Jwt:Key"]!);

            var validationParams = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = config["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = config["Jwt:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30),
                NameClaimType = ClaimTypes.Name,
                RoleClaimType = ClaimTypes.Role
            };

            var principal = tokenHandler.ValidateToken(token, validationParams, out SecurityToken validatedToken);
            return principal;
        }

        public static (string? UserId, string? Username, string? Jti) ValidateAndRead(string token, IConfiguration config)
        {
            var principal = ValidateTokenAndGetPrincipal(token, config);
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = principal.Identity?.Name;
            var jti = principal.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            return (userId, username, jti);
        }
    }
}
