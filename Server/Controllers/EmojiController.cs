using Cozy_Chatter.DTO;
using Cozy_Chatter.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cozy_Chatter.Controllers
{
    public class EmojiController(IEmojiRepository emojiRepository, ISMPostRepository postRepository) : AbstractController
    {
        private readonly IEmojiRepository _emojiRepository = emojiRepository;
        private readonly ISMPostRepository _postRepository = postRepository;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetEmojis([FromQuery] PaginationRequest request)
        {
            return await GetPagedData(
                request,
                40,
                80,
                _emojiRepository,
                await _emojiRepository.GetPagedAsync(request.PageNumber, request.PageSize)
            );
        }

        //Get social media post reactions
        [HttpGet("{postid}/reactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSMPostReactions(int postid)
        {
            if (postid <= 0) return BadRequest("Post ID must be a positive integer");
            if (await _postRepository.GetPostById(postid) == null) return NotFound("Post not found");
            var reactions = await _emojiRepository.GetReactionsByPostId(postid);
            if (reactions.Count == 0) return NoContent();
            return Ok(reactions.GroupBy(r => r.EmojiId));
        }
    }
}