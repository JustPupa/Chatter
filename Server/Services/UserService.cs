using Cozy_Chatter.DTO;
using Cozy_Chatter.Models;
using Cozy_Chatter.Repositories;
using Cozy_Chatter.Services.Interfaces;

namespace Cozy_Chatter.Services
{
    public class UserService(IUserRepository userRepository, ICredentialRepository credentialRepository,
        IProfilePictureRepository profilePictureRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICredentialRepository _credentialRepository = credentialRepository;
        private readonly IProfilePictureRepository _profilePictureRepository = profilePictureRepository;

        public async Task<int> GetCountAsync<T>() where T : IEntity
        {
            return typeof(T) switch
            {
                Type t when t == typeof(User) => await _userRepository.GetCountAsync(),
                Type t when t == typeof(Credential) => await _credentialRepository.GetCountAsync(),
                Type t when t == typeof(Pfpicture) => await _profilePictureRepository.GetCountAsync(),
                _ => throw new NotSupportedException($"GetCountAsync is not supported for type {typeof(T)}")
            };
        }
        public async Task<User?> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }
        public async Task<List<User>> GetSubscribersByUser(int userid, PaginationRequest request)
        {
            return await _userRepository.GetSubscribersByUserId(
                userid,
                request.PageNumber,
                request.PageSize
            );
        }
        public async Task<User?> ValidateUserAsync(string login, string password)
        {
            return await _credentialRepository.ValidateUserAsync(login, password);
        }
        public async Task<List<Pfpicture>> GetProfilePicturesByUserId(int id, PaginationRequest request)
        {
            return await _profilePictureRepository.GetProfilePicturesByUserId(
                id, 
                request.PageNumber, 
                request.PageSize
            );
        }
    }
}