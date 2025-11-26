using Cozy_Chatter.Models;
using Microsoft.EntityFrameworkCore;

namespace Cozy_Chatter.Repositories
{
    public class UserRepository(ChatterContext context) : AbstractRepository(context)
    {
        public override async Task<int> GetCountAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>> GetSubscribersByUserId(int id, int pageNumber, int pageSize)
        {
            var subscribersIds = _context.Subscriptions
                .Where(s => s.UserId == id)
                .OrderByDescending(s => s.Date)
                .Select(s => s.FollowerId);
            return await _context.Users
                .Where(u => subscribersIds.Contains(u.Id))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}