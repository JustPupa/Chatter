using Cozy_Chatter.Models;

namespace Cozy_Chatter.Services.Interfaces
{
    public interface IService
    {
        Task<int> GetCountAsync<T>() where T : IEntity;
    }
}