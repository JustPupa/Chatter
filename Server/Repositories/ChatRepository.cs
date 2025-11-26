using Cozy_Chatter.Models;
using Microsoft.EntityFrameworkCore;

namespace Cozy_Chatter.Repositories
{
    public class ChatRepository(ChatterContext context) : AbstractRepository(context)
    {
        public override async Task<int> GetCountAsync()
        {
            return await _context.Chats.CountAsync();
        }

        public async Task<Chat?> GetChatsById(int id)
        {
            return await _context.Chats.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<List<Chat>> GetChatsByUserId(int userId, int pageNumber, int pageSize)
        {
            return await _context.Chats
                .Include(c => c.ChatUsers)
                .Where(c => c.ChatUsers.Any(u => u.UserId == userId))
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<User>> GetUsersByChatId(int chatId, int pageNumber, int pageSize)
        {
            return await _context.Users
                .Include(u => u.ChatUsers)
                .Where(u => u.ChatUsers.Any(c => c.ChatId == chatId))
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}