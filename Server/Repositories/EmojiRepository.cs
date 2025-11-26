using Cozy_Chatter.Models;
using Microsoft.EntityFrameworkCore;

namespace Cozy_Chatter.Repositories
{
    public class EmojiRepository(ChatterContext context) : AbstractRepository(context)
    {
        public override async Task<int> GetCountAsync()
        {
            return await _context.Emojis.CountAsync();
        }

        public async Task<List<Emoji>> GetAllEmojis()
        {
            return await _context.Emojis.ToListAsync();
        }

        public async Task<List<Emoji>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.Emojis
                .OrderBy(e => e.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<UserReaction>> GetReactionsByPostId(int id)
        {
            return await _context.UserReactions.Where(ur => ur.PostId == id).ToListAsync();
        }
    }
}