using Cozy_Chatter.Models;
using Microsoft.EntityFrameworkCore;

namespace Cozy_Chatter.Repositories
{
    public class ChatRepository(ChatterContext context)
    {
        private readonly ChatterContext _context = context;
        public async Task<List<Chat>?> GetChatsByUserId(int id)
        {
            var chatsIds = _context.ChatUsers.Where(cu => cu.UserId == id).Select(cu => cu.ChatId);
            return await _context.Chats.Where(c => chatsIds.Contains(c.Id)).ToListAsync();
        }

        public async Task<List<User>?> GetUsersByChatId(int id)
        {
            var usersIds = _context.ChatUsers.Where(cu => cu.ChatId == id).Select(cu => cu.UserId);
            return await _context.Users.Where(u => usersIds.Contains(u.Id)).ToListAsync();
        }

        public async Task<List<Message>?> GetMessagesByChatId(int id)
        {
            return await _context.Messages.Where(m => m.ChatId == id).ToListAsync();
        }

        public async Task<List<Message>?> GetChatPinnedMessages(int chatId)
        {
            return await _context.Messages
                .Where(m => m.ChatId == chatId && m.IsPublic == true)
                .ToListAsync();
        }
    }
}
