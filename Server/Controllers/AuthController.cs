using Cozy_Chatter.Repositories;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cozy_Chatter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(CredentialRepository credentialRepository) : ControllerBase
    {
        private readonly CredentialRepository _credentialRepository = credentialRepository;

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (_credentialRepository.CheckCredentialsExist(request.Login, request.Password).Result)
            {
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("HrnRMUe5YAhwGz5NVFdq3zbyb5klpzxl"));
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
}

public record LoginRequest(string Login, string Password);