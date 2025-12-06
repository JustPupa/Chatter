using Cozy_Chatter.Data;

namespace Cozy_Chatter.Repositories
{
    public abstract class AbstractRepository(ChatterContext context) : IRepository, IDisposable
    {
        protected readonly ChatterContext _context = context;
        private bool disposed = false;
        public abstract Task<int> GetCountAsync();
        public virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing) _context.Dispose();
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public interface IRepository
    {
        Task<int> GetCountAsync();
    }
}