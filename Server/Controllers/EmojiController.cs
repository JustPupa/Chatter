using Cozy_Chatter.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cozy_Chatter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmojiController(EmojiRepository emojiRepository, SMPostRepository postRepository) : ControllerBase
    {
        private readonly EmojiRepository _emojiRepository = emojiRepository;
        private readonly SMPostRepository _postRepository = postRepository;

        //Get list of all emojis
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetEmojis()
        {
            var emojis = await _emojiRepository.GetAllEmojis();
            if (emojis.Count == 0) return NoContent();
            return Ok(emojis);
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
            return Ok(reactions);
        }
    }
}