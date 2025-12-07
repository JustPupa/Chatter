using Cozy_Chatter.DTO;
using Cozy_Chatter.Models;
using Cozy_Chatter.Repositories;
using Cozy_Chatter.Services.Interfaces;

namespace Cozy_Chatter.Services
{
    public class SMPostService(ISMPostRepository smPostRepository, IEmojiRepository emojiRepository,
        IUserRepository userRepository) : ISMPostService
    {
        private readonly ISMPostRepository _smPostRepository = smPostRepository;
        private readonly IEmojiRepository _emojiRepository = emojiRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<int> GetCountAsync<T>() where T : IEntity
        {
            return typeof(T) switch
            {
                Type t when t == typeof(SMPost) => await _smPostRepository.GetCountAsync(),
                Type t when t == typeof(Emoji) => await _emojiRepository.GetCountAsync(),
                Type t when t == typeof(User) => await _userRepository.GetCountAsync(),
                Type t when t == typeof(SMPostLike) => await _smPostRepository.GetLikesCountAsync(),
                Type t when t == typeof(UserReaction) => await _smPostRepository.GetReactionsCountAsync(),
                _ => throw new NotSupportedException($"GetCountAsync is not supported for type {typeof(T)}")
            };
        }
        public async Task<List<Emoji>> GetAllEmojis(PaginationRequest request)
        {
            return await _emojiRepository.GetAllEmojis(request.PageNumber, request.PageSize);
        }
        public async Task<SMPost?> GetPostById(int id)
        {
            return await _smPostRepository.GetPostById(id);
        }
        public async Task<List<UserReaction>> GetReactionsByPostId(int id)
        {
            return await _emojiRepository.GetReactionsByPostId(id);
        }
        public async Task<List<SMPost>> GetDetailedLatestPosts()
        {
            return await _smPostRepository.GetDetailedLatestPosts();
        }
        public async Task<User?> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }
        public async Task<List<SMPost>> GetDetailedPostsByUserId(int userId, PaginationRequest request)
        {
            return await _smPostRepository.GetDetailedPostsByUserId(userId, request.PageNumber, request.PageSize);
        }
        public async Task<List<SMPostLike>> GetLikesByPostId(int postId, PaginationRequest request)
        {
            return await _smPostRepository.GetLikesByPostId(postId, request.PageNumber, request.PageSize);
        }
    }
}