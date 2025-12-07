using Cozy_Chatter.DTO;
using Cozy_Chatter.Models;

namespace Cozy_Chatter.Services.Interfaces
{
    public interface IUserService : IService
    {
        Task<User?> GetUserById(int id);
        Task<List<User>> GetSubscribersByUser(int userid, PaginationRequest request);
        Task<User?> ValidateUserAsync(string login, string password);
        Task<List<Pfpicture>> GetProfilePicturesByUserId(int id, PaginationRequest request);
    };
}