using Cozy_Chatter.DTO;
using Cozy_Chatter.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cozy_Chatter.Controllers
{
    public class ProfilePictureController(IUserService userService) : AbstractController
    {
        private readonly IUserService _userService = userService;

        [HttpGet("{userid}/pfpictures")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserPfps(int userid, [FromQuery] PaginationRequest request)
        {
            if (userid <= 0) return BadRequest("User ID must be a positive integer");
            if (await _userService.GetUserById(userid) == null) return NotFound("User not found");
            return await GetPagedData(
                request,
                1,
                20,
                _userService,
                await _userService.GetProfilePicturesByUserId(userid, request)
            );
        }
    }
}