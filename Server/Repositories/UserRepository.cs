using Cozy_Chatter.Data;
using Cozy_Chatter.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Cozy_Chatter.Repositories
{
    public class UserRepository(ChatterContext context) : AbstractRepository(context), IUserRepository
    {
        public override async Task<int> GetCountAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> GetUserByIdAndChatAsync(int id, int chatId)
        {
            var chatUsers = await _context.ChatUsers.Where(cu => cu.ChatId == chatId).Select(cu => cu.UserId).ToListAsync();
            return await _context.Users.AnyAsync(u => u.Id == id && chatUsers.Contains(id));
        }

        public async Task<List<User>> GetSubscribersByUserId(int id, int pageNumber, int pageSize)
        {
            var subscribersIds = _context.Subscriptions
                .Where(s => s.UserId == id)
                .OrderByDescending(s => s.Date)
                .Select(s => s.FollowerId);
            return await _context.Users
                .Where(u => subscribersIds.Contains(u.Id))
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }

    public interface IUserRepository : IRepository
    {
        Task<User?> GetUserById(int id);
        Task<bool> GetUserByIdAndChatAsync(int id, int chatId);
        Task<List<User>> GetSubscribersByUserId(int id, int pageNumber, int pageSize);
    }
}