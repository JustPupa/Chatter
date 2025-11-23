using Cozy_Chatter.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cozy_Chatter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(CredentialRepository credentialRepository, IConfiguration configuration) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly CredentialRepository _credentialRepository = credentialRepository;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            bool credentialsExist = await _credentialRepository.CheckCredentialsExist(request.Login, request.Password);
            if (credentialsExist)
            {
                var jwtKey = _configuration["Jwt:Key"]
                     ?? Environment.GetEnvironmentVariable("Jwt__Key")
                     ?? throw new Exception("JWT Key not configured");
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    claims: [new Claim(ClaimTypes.Name, request.Login)],
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            return Unauthorized(new { message = "Invalid credentials" });
        }
    }

    public record LoginRequest(string Login, string Password);
}