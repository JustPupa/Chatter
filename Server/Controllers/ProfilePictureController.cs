using Cozy_Chatter.DTO;
using Cozy_Chatter.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cozy_Chatter.Controllers
{
    public class ProfilePictureController(ProfilePictureRepository pfpRepository,
        UserRepository userRepository) : AbstractController
    {
        private readonly ProfilePictureRepository _pfpRepository = pfpRepository;
        private readonly UserRepository _userRepository = userRepository;

        [HttpGet("{userid}/pfpictures")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserPfps(int userid, [FromQuery] PaginationRequest request)
        {
            if (userid <= 0) return BadRequest("User ID must be a positive integer");
            if (await _userRepository.GetUserById(userid) == null) return NotFound("User not found");
            return await GetPagedData(
                request,
                1,
                20,
                _pfpRepository,
                await _pfpRepository.GetProfilePicturesByUserId(userid, request.PageNumber, request.PageSize)
            );
        }
    }
}