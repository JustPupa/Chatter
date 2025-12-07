using Cozy_Chatter.DTO;
using Cozy_Chatter.Models;

namespace Cozy_Chatter.Services.Interfaces
{
    public interface ISMPostService : IService
    {
        Task<SMPost?> GetPostById(int id);
        Task<List<UserReaction>> GetReactionsByPostId(int id);
        Task<List<SMPost>> GetDetailedLatestPosts();
        Task<User?> GetUserById(int id);
        Task<List<SMPost>> GetDetailedPostsByUserId(int id, PaginationRequest request);
        Task<List<SMPostLike>> GetLikesByPostId(int id, PaginationRequest request);
        Task<List<Emoji>> GetAllEmojis(PaginationRequest request);
    };
}