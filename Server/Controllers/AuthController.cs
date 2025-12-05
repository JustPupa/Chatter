using Cozy_Chatter.Repositories;
using Cozy_Chatter.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cozy_Chatter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(CredentialRepository credentialRepository,
        ITokenService _tokenService, UserRepository userRepository) : ControllerBase
    {
        private readonly CredentialRepository _credentialRepository = credentialRepository;
        private readonly ITokenService _tokenService = _tokenService;
        private readonly UserRepository _userRepository = userRepository;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _credentialRepository.ValidateUserAsync(request.Login, request.Password);
            if (user == null) return Unauthorized(new { message = "Invalid credentials" });
            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken(user);
            Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });
            return Ok(new {
                accessToken
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken)) return Unauthorized();
            var principal = _tokenService.ValidateRefreshToken(refreshToken);
            if (principal == null) return Unauthorized();
            var userIdString = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdString == null || !int.TryParse(userIdString, out var userId)) return Unauthorized();
            var user = await _userRepository.GetUserById(userId);
            if (user == null) return Unauthorized();
            var newAccess = _tokenService.GenerateAccessToken(user);
            var newRefresh = _tokenService.GenerateRefreshToken(user);
            Response.Cookies.Append("refreshToken", newRefresh, new CookieOptions {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });
            return Ok(new { accessToken = newAccess });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            if (Request.Cookies.ContainsKey("refreshToken"))
            {
                Response.Cookies.Append("refreshToken", "", new CookieOptions {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(-1)
                });
            }
            return NoContent();
        }
    }
    public record LoginRequest(string Login, string Password);
}