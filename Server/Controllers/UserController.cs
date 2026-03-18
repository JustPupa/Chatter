using Cozy_Chatter.DTO;
using Cozy_Chatter.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cozy_Chatter.Controllers
{
    public class UserController(IUserService userService) : AbstractController
    {
        private readonly IUserService _userService = userService;

        [HttpGet("{userid}/subscribers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSubscribersByUser(int userid, [FromQuery] PaginationRequest request)
        {
            if (userid <= 0) return BadRequest("User ID must be a positive integer");
            if (await _userService.GetUserById(userid) == null) return NotFound("User not found");
            return await GetPagedData(
                request,
                30,
                50,
                _userService,
                await _userService.GetSubscribersByUser(userid, request)
            );
        }

        [HttpGet("account")]
        public IActionResult GetAccount()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();
            return Ok(new
            {
                id = userId,
                login = User.Identity?.Name
            });
        }
    }
}