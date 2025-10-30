using Cozy_Chatter.Models;
using Microsoft.EntityFrameworkCore;

namespace Cozy_Chatter.Repositories
{
    public class UserRepository
    {
        public static async Task<User?> GetUserById(int id)
        {
            using var context = new ChatterContext();
            return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public static async Task<List<User>?> GetSubscribersByUserId(int id)
        {
            using var context = new ChatterContext();
            var subscribersIds = context.Subscriptions
                .Where(s => s.UserId == id)
                .Select(s => s.FollowerId);
            return await context.Users.Where(u => subscribersIds.Contains(u.Id)).ToListAsync();
        }

        public static async Task<List<int>?> GetProfilePicturesByUserId(int id)
        {
            using var context = new ChatterContext();
            return await context.Pfpictures
                .Where(pfp => pfp.UserId == id)
                .Select(pfp => pfp.PictureId)
                .ToListAsync();
        }
    }
}