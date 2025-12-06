using Cozy_Chatter.Data;
using Cozy_Chatter.Models;
using Microsoft.EntityFrameworkCore;

namespace Cozy_Chatter.Repositories
{
    public class MessageRepository(ChatterContext context) : AbstractRepository(context), IMessageRepository
    {
        public override async Task<int> GetCountAsync()
        {
            return await _context.Messages.CountAsync();
        }

        public async Task<int> GetCountByChatAsync(int chatid)
        {
            return await _context.Messages
                .Where(m => m.ChatId == chatid)
                .CountAsync();
        } 

        public async Task<int> GetPinnedCountByChatAsync(int chatid)
        {
            return await _context.Messages
                .Where(m => m.ChatId == chatid && m.IsPublic == true)
                .CountAsync();
        }

        public async Task<List<Message>> GetMessagesByChatId(int id, int pageNumber, int pageSize)
        {
            return await _context.Messages
                .Where(m => m.ChatId == id)
                .OrderByDescending(m => m.TimeStamp)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Message>> GetChatPinnedMessages(int chatId, int pageNumber, int pageSize)
        {
            return await _context.Messages
                .Where(m => m.ChatId == chatId && m.IsPublic == true)
                .OrderByDescending(m => m.TimeStamp)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }

    public interface IMessageRepository : IRepository
    {
        Task<int> GetCountByChatAsync(int chatid);
        Task<int> GetPinnedCountByChatAsync(int chatid);
        Task<List<Message>> GetMessagesByChatId(int id, int pageNumber, int pageSize);
        Task<List<Message>> GetChatPinnedMessages(int chatId, int pageNumber, int pageSize);
    }
}