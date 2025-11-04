using Cozy_Chatter.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cozy_Chatter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmojiController(EmojiRepository emojiRepository) : ControllerBase
    {
        private readonly EmojiRepository _emojiRepository = emojiRepository;

        //Get list of all emojis
        [HttpGet]
        public async Task<IActionResult> GetEmojis()
        {
            var emojis = await _emojiRepository.GetAllEmojis();
            return emojis == null ?
                BadRequest("Emojis cannot be loaded") : Ok(emojis);
        }
    }
}