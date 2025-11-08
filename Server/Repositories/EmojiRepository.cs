using Cozy_Chatter.Models;
using Microsoft.EntityFrameworkCore;

namespace Cozy_Chatter.Repositories
{
    public class EmojiRepository(ChatterContext context)
    {
        private readonly ChatterContext _context = context;

        public async Task<List<Emoji>> GetAllEmojis()
        {
            return await _context.Emojis.ToListAsync();
        }

        public async Task<List<UserReaction>> GetReactionsByPostId(int id)
        {
            return await _context.UserReactions.Where(ur => ur.PostId == id).ToListAsync();
        }
    }
}