using Cozy_Chatter.DTO;
using Cozy_Chatter.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cozy_Chatter.Controllers
{
    public class SMPostController(ISMPostService smPostService) : AbstractController
    {
        private readonly ISMPostService _smPostService = smPostService;

        [HttpGet("latest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetLatestPosts()
        {
            var latestPosts = await _smPostService.GetDetailedLatestPosts();
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
            if (await _smPostService.GetUserById(userid) == null) return NotFound("User not found");
            return await GetPagedData(
                request,
                1,
                50,
                _smPostService,
                await _smPostService.GetDetailedPostsByUserId(userid, request)
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
            if (await _smPostService.GetPostById(postid) == null) return NotFound("Post not found");
            return await GetPagedData(
                request,
                10,
                50,
                _smPostService,
                await _smPostService.GetLikesByPostId(postid, request)
            );
        }
    }
}