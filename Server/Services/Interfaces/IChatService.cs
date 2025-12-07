using Cozy_Chatter.DTO;
using Cozy_Chatter.Models;

namespace Cozy_Chatter.Services.Interfaces
{
    public interface IChatService : IService
    {
        Task<User?> GetUserById(int id);
        Task<Chat?> GetChatsById(int id);
        Task<List<Chat>> GetChatsByUserId(int userId, PaginationRequest request);
        Task<List<User>> GetUsersByChatId(int chatId, PaginationRequest request);
        Task<List<Message>> GetMessagesByChatId(int id, PaginationRequest request);
        Task<int> GetPinnedCountByChatAsync(int chatid);
    };
}