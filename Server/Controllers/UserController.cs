using Cozy_Chatter.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cozy_Chatter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(UserRepository userRepository) : ControllerBase
    {
        private readonly UserRepository _userRepository = userRepository;

        //Get subscribers by user
        [HttpGet("{userid}/subscribers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSubscribersByUser(int userid)
        {
            if (userid <= 0) return BadRequest("User ID must be a positive integer");
            if (await _userRepository.GetUserById(userid) == null) return NotFound("User not found");
            var subscribers = await _userRepository.GetSubscribersByUserId(userid);
            if (subscribers.Count == 0) return NoContent();
            return Ok(subscribers);
        }

        //Get user profile pictures (!!!)
        [HttpGet("{userid}/pfpicture")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserPfps(int userid)
        {
            if (userid <= 0) return BadRequest("User ID must be a positive integer");
            if (await _userRepository.GetUserById(userid) == null) return NotFound("User not found");
            var profilePictures = await _userRepository.GetProfilePicturesByUserId(userid);
            if (profilePictures.Count == 0) return NoContent();
            return Ok(profilePictures);
        }
    }
}