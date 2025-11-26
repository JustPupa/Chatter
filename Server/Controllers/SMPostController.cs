using Cozy_Chatter.DTO;
using Cozy_Chatter.Repositories; 
using Microsoft.AspNetCore.Mvc;

namespace Cozy_Chatter.Controllers
{
    public class SMPostController(SMPostRepository postRepository, UserRepository userRepository) : AbstractController
    {
        private readonly SMPostRepository _postRepository = postRepository;
        private readonly UserRepository _userRepository = userRepository;

        [HttpGet("latest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetLatestPosts()
        {
            var latestPosts = await _postRepository.GetDetailedLatestPosts();
            if (latestPosts.Count == 0) return NoContent();
            return Ok(latestPosts);
        }

        [HttpGet("{userid}/posts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserPosts(int userid, [FromQuery] PaginationRequest request)
        {
            if (userid <= 0) return BadRequest("User ID must be a positive integer");
            if (await _userRepository.GetUserById(userid) == null) return NotFound("User not found");
            return await GetPagedData(
                request,
                1,
                50,
                _postRepository,
                await _postRepository.GetDetailedPostsByUserId(userid, request.PageNumber, request.PageSize)
            );
        }

        [HttpGet("{postid}/likes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSMPostLikes(int postid, [FromQuery] PaginationRequest request)
        {
            if (postid <= 0) return BadRequest("Post ID must be a positive integer");
            if (await _postRepository.GetPostById(postid) == null) return NotFound("Post not found");
            return await GetPagedData(
                request,
                10,
                50,
                _postRepository,
                await _postRepository.GetLikesByPostId(postid, request.PageNumber, request.PageSize)
            );
        }
    }
}