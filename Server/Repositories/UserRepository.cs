using Cozy_Chatter.Models;
using Microsoft.EntityFrameworkCore;

namespace Cozy_Chatter.Repositories
{
    public class UserRepository(ChatterContext context)
    {
        private readonly ChatterContext _context = context;
        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>?> GetSubscribersByUserId(int id)
        {
            var subscribersIds = _context.Subscriptions
                .Where(s => s.UserId == id)
                .Select(s => s.FollowerId);
            return await _context.Users.Where(u => subscribersIds.Contains(u.Id)).ToListAsync();
        }

        public async Task<List<int>?> GetProfilePicturesByUserId(int id)
        {
            return await _context.Pfpictures
                .Where(pfp => pfp.UserId == id)
                .Select(pfp => pfp.PictureId)
                .ToListAsync();
        }
    }
}