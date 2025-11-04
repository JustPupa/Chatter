using Cozy_Chatter.Repositories; 
using Microsoft.AspNetCore.Mvc;

namespace Cozy_Chatter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SMPostController(SMPostRepository postRepository) : ControllerBase
    {
        private readonly SMPostRepository _postRepository = postRepository;

        //Get all posts
        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestPosts()
        {
            var latestPosts = await _postRepository.GetDetailedLatestPosts();
            return latestPosts == null ? 
                BadRequest("The feed cannot be uploaded") : Ok(latestPosts);
        }

        //Get posts of a specific user
        [HttpGet("{userid}/posts")]
        public async Task<IActionResult> GetUserPosts(int userid)
        {
            var userPosts = await _postRepository.GetDetailedPostsByUserId(userid);
            return userPosts == null ?
                BadRequest("User's posts cannot be loaded") : Ok(userPosts);
        }

        //Get social media post reactions
        [HttpGet("{postid}/reactions")]
        public async Task<IActionResult> GetSMPostReactions(int postid)
        {
            var reactions = await EmojiRepository.GetReactionsByPostId(postid);
            return reactions == null ?
                BadRequest("Social media post reactions information cannot be loaded") : Ok(reactions);
        }

        //Get social media post likes
        [HttpGet("{postid}/likes")]
        public async Task<IActionResult> GetSMPostLikes(int postid)
        {
            var likes = await _postRepository.GetLikesByPostId(postid);
            return likes == null ?
                BadRequest("Social media post likes information cannot be loaded") : Ok(likes);
        }
    }
}