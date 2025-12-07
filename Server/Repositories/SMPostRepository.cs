using Cozy_Chatter.Data;
using Cozy_Chatter.Models;
using Microsoft.EntityFrameworkCore;

namespace Cozy_Chatter.Repositories
{
    public class SMPostRepository(ChatterContext context) : AbstractRepository(context), ISMPostRepository
    {
        public override async Task<int> GetCountAsync()
        {
            return await _context.SMPosts.CountAsync();
        }

        public async Task<int> GetLikesCountAsync()
        {
            return await _context.SMPostLikes.CountAsync();
        }

        public async Task<int> GetReactionsCountAsync()
        {
            return await _context.UserReactions.CountAsync();
        }

        public async Task<SMPost?> GetPostById(int id)
        {
            return await _context.SMPosts.FirstOrDefaultAsync(smp => smp.Id == id);
        }

        public async Task<List<SMPost>> GetDetailedPostsByUserId(int id, int pageNumber, int pageSize)
        {
            return await _context.SMPosts
                .Where(p => p.UserId == id)
                .OrderByDescending(p => p.Pub_time)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(ps => ps.Publisher)
                .Include(ps => ps.ReferencePost)
                .ToListAsync();
        }

        public async Task<List<SMPost>> GetDetailedLatestPosts()
        {
            return await _context.SMPosts
                .OrderByDescending(p => p.Pub_time)
                .Take(100)
                .Include(ps => ps.Publisher)
                .Include(ps => ps.ReferencePost)
                .ToListAsync();
        }

        public async Task<List<SMPostLike>> GetLikesByPostId(int id, int pageNumber, int pageSize)
        {
            return await _context.SMPostLikes
                .Where(pl => pl.PostId == id)
                .OrderBy(pl => pl.PostId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }

    public interface ISMPostRepository : IRepository
    {
        Task<int> GetLikesCountAsync();
        Task<int> GetReactionsCountAsync();
        Task<SMPost?> GetPostById(int id);
        Task<List<SMPost>> GetDetailedPostsByUserId(int id, int pageNumber, int pageSize);
        Task<List<SMPost>> GetDetailedLatestPosts();
        Task<List<SMPostLike>> GetLikesByPostId(int id, int pageNumber, int pageSize);
    }
}