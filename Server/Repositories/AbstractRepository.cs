namespace Cozy_Chatter.Repositories
{
    public abstract class AbstractRepository(ChatterContext context)
    {
        protected readonly ChatterContext _context = context;
        public abstract Task<int> GetCountAsync();
    }
}