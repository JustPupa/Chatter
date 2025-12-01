using Cozy_Chatter.Data;

namespace Cozy_Chatter.Repositories
{
    public abstract class AbstractRepository(ChatterContext context) : IRepository
    {
        protected readonly ChatterContext _context = context;
        public abstract Task<int> GetCountAsync();
    }

    public interface IRepository
    {
        Task<int> GetCountAsync();
    }
}