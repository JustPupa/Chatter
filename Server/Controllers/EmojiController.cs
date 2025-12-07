using Cozy_Chatter.DTO;
using Cozy_Chatter.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cozy_Chatter.Controllers
{
    public class EmojiController(ISMPostService smPostService) : AbstractController
    {
        private readonly ISMPostService _smPostService = smPostService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetEmojis([FromQuery] PaginationRequest request)
        {
            return await GetPagedData(
                request,
                40,
                80,
                _smPostService,
                await _smPostService.GetAllEmojis(request)
            );
        }

        [HttpGet("{postid}/reactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSMPostReactions(int postid)
        {
            if (postid <= 0) return BadRequest("Post ID must be a positive integer");
            if (await _smPostService.GetPostById(postid) == null) return NotFound("Post not found");
            var reactions = await _smPostService.GetReactionsByPostId(postid);
            if (reactions.Count == 0) return NoContent();
            return Ok(reactions.GroupBy(r => r.EmojiId));
        }
    }
}