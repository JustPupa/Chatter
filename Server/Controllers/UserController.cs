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
        public async Task<IActionResult> GetSubscribersByUser(int userid)
        {
            var subscribers = await _userRepository.GetSubscribersByUserId(userid);
            return subscribers == null ?
                BadRequest("List of subscribers cannot be loaded") : Ok(subscribers);
        }

        //Get user profile pictures (!!!)
        [HttpGet("{userid}/pfpicture")]
        public async Task<IActionResult> GetUserPfps(int userid)
        {
            var profilePictures = await _userRepository.GetProfilePicturesByUserId(userid);
            return profilePictures == null ?
                BadRequest("User profile pictures cannot be loaded") : Ok(profilePictures);
        }
    }
}