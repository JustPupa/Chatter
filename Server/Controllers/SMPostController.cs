using Cozy_Chatter.Repositories; 
using Microsoft.AspNetCore.Mvc;

namespace Cozy_Chatter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SMPostController(SMPostRepository postRepository, UserRepository userRepository) : ControllerBase
    {
        private readonly SMPostRepository _postRepository = postRepository;
        private readonly UserRepository _userRepository = userRepository;

        //Get all posts
        [HttpGet("latest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetLatestPosts()
        {
            var latestPosts = await _postRepository.GetDetailedLatestPosts();
            if (latestPosts.Count == 0) return NoContent();
            return Ok(latestPosts);
        }

        //Get posts of a specific user
        [HttpGet("{userid}/posts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserPosts(int userid)
        {
            if (userid <= 0) return BadRequest("User ID must be a positive integer");
            if (await _userRepository.GetUserById(userid) == null) return NotFound("User not found");
            var userPosts = await _postRepository.GetDetailedPostsByUserId(userid);
            if (userPosts.Count == 0) return NoContent();
            return Ok(userPosts);
        }

        //Get social media post likes
        [HttpGet("{postid}/likes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSMPostLikes(int postid)
        {
            if (postid <= 0) return BadRequest("Post ID must be a positive integer");
            if (await _postRepository.GetPostById(postid) == null) return NotFound("Post not found");
            var likes = await _postRepository.GetLikesByPostId(postid);
            if (likes.Count == 0) return NoContent();
            return Ok(likes);
        }
    }
}