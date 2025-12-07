using Cozy_Chatter.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Cozy_Chatter.Services.Interfaces
{
    public interface ITokenService
    {
        public SymmetricSecurityKey GenerageKey();
        public string GenerateAccessToken(User user);
        public string GenerateRefreshToken(User user);
        public ClaimsPrincipal? ValidateRefreshToken(string token);
    }
}