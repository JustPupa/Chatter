using Cozy_Chatter.Models;
using Microsoft.EntityFrameworkCore;

namespace Cozy_Chatter.Repositories
{
    public class ChatRepository
    {
        public static async Task<List<Chat>?> GetChatsByUserId(int id)
        {
            using var context = new ChatterContext();
            var chatsIds = context.ChatUsers.Where(cu => cu.UserId == id).Select(cu => cu.ChatId);
            return await context.Chats.Where(c => chatsIds.Contains(c.Id)).ToListAsync();
        }

        public static async Task<List<User>?> GetUsersByChatId(int id)
        {
            using var context = new ChatterContext();
            var usersIds = context.ChatUsers.Where(cu => cu.ChatId == id).Select(cu => cu.UserId);
            return await context.Users.Where(u => usersIds.Contains(u.Id)).ToListAsync();
        }

        public static async Task<List<Message>?> GetMessagesByChatId(int id)
        {
            using var context = new ChatterContext();
            return await context.Messages.Where(m => m.ChatId == id).ToListAsync();
        }

        public static async Task<List<Message>?> GetChatPinnedMessages(int chatId)
        {
            using var context = new ChatterContext();
            return await context.Messages
                .Where(m => m.ChatId == chatId && m.IsPublic == true)
                .ToListAsync();
        }
    }
}
