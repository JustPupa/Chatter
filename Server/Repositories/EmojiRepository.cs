using Cozy_Chatter.DTOs;
using Cozy_Chatter.Models;
using Microsoft.EntityFrameworkCore;

namespace Cozy_Chatter.Repositories
{
    public class EmojiRepository
    {
        public static async Task<List<UserReaction>?> GetReactionsByPostId(int id)
        {
            using var context = new ChatterContext();
            return await context.UserReactions.Where(ur => ur.PostId == id).ToListAsync();
        }

        public static async Task<List<Emoji>?> GetAllEmojis()
        {
            using var context = new ChatterContext();
            return await context.Emojis.ToListAsync();
        }
    }
}
