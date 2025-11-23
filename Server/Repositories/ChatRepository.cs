using Cozy_Chatter.Models;
using Microsoft.EntityFrameworkCore;

namespace Cozy_Chatter.Repositories
{
    public class ChatRepository(ChatterContext context)
    {
        private readonly ChatterContext _context = context;

        public async Task<Chat?> GetChatsById(int id)
        {
            return await _context.Chats.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<List<Chat>> GetChatsByUserId(int userId)
        {
            return await _context.Chats
                .Include(c => c.ChatUsers)
                .Where(c => c.ChatUsers.Any(u => u.UserId == userId))
                .ToListAsync();
        }

        public async Task<List<User>> GetUsersByChatId(int chatId)
        {
            return await _context.Users
                .Include(u => u.ChatUsers)
                .Where(u => u.ChatUsers.Any(c => c.ChatId == chatId))
                .ToListAsync();
        }

        public async Task<List<Message>> GetMessagesByChatId(int id)
        {
            return await _context.Messages.Where(m => m.ChatId == id).ToListAsync();
        }

        public async Task<List<Message>> GetChatPinnedMessages(int chatId)
        {
            return await _context.Messages
                .Where(m => m.ChatId == chatId && m.IsPublic == true)
                .ToListAsync();
        }
    }
}
