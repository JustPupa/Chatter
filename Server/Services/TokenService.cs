using Cozy_Chatter.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cozy_Chatter.Services
{
    public interface ITokenService
    {
        public SymmetricSecurityKey GenerageKey();
        public string GenerateAccessToken(User user);
        public string GenerateRefreshToken(User user);
        public ClaimsPrincipal? ValidateRefreshToken(string token);
    }
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _key = GenerageKey();
        }

        public SymmetricSecurityKey GenerageKey()
        {
            var jwtKey = _configuration["Jwt:Key"]
                 ?? Environment.GetEnvironmentVariable("Jwt__Key")
                 ?? throw new Exception("JWT Key not configured");
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey));
        }

        public string GenerateAccessToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.DisplayedName??"")
            };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken(User user)
        {
            var claims = new[] {
                new Claim("type", "refresh"),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal? ValidateRefreshToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var principal = handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _key,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);
                if (principal.HasClaim(c => c.Type == "type" && c.Value == "refresh")) return principal;
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}