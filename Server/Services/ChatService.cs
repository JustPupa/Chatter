using Cozy_Chatter.DTO;
using Cozy_Chatter.Models;
using Cozy_Chatter.Repositories;
using Cozy_Chatter.Services.Interfaces;

namespace Cozy_Chatter.Services
{
    public class ChatService(IChatRepository chatRepository, IMessageRepository messageRepository,
        IUserRepository userRepository) : IChatService
    {
        private readonly IChatRepository _chatRepository = chatRepository;
        private readonly IMessageRepository _messageRepository = messageRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<int> GetCountAsync<T>() where T : IEntity
        {
            return typeof(T) switch
            {
                Type t when t == typeof(Chat) => await _chatRepository.GetCountAsync(),
                Type t when t == typeof(Message) => await _messageRepository.GetCountAsync(),
                Type t when t == typeof(User) => await _userRepository.GetCountAsync(),
                _ => throw new NotSupportedException($"GetCountAsync is not supported for type {typeof(T)}")
            };
        }
        public async Task<User?> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }
        public async Task<Chat?> GetChatsById(int id)
        {
            return await _chatRepository.GetChatsById(id);
        }
        public async Task<List<Chat>> GetChatsByUserId(int userId, PaginationRequest request)
        {
            return await _chatRepository.GetChatsByUserId(userId, request.PageNumber, request.PageSize);
        }
        public async Task<List<User>> GetUsersByChatId(int chatId, PaginationRequest request)
        {
            return await _chatRepository.GetUsersByChatId(chatId, request.PageNumber, request.PageSize);
        }
        public async Task<List<Message>> GetMessagesByChatId(int id, PaginationRequest request)
        {
            return await _messageRepository.GetMessagesByChatId(id, request.PageNumber, request.PageSize);
        }
        public async Task<int> GetPinnedCountByChatAsync(int chatid)
        {
            return await _messageRepository.GetPinnedCountByChatAsync(chatid);
        }
    }
}