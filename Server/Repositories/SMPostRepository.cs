using Cozy_Chatter.Data;
using Cozy_Chatter.Models;
using Microsoft.EntityFrameworkCore;

namespace Cozy_Chatter.Repositories
{
    public class SMPostRepository(ChatterContext context) : AbstractRepository(context)
    {
        public override async Task<int> GetCountAsync()
        {
            return await _context.SMPosts.CountAsync();
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

        //Select only TOP 100
        public async Task<List<SMPost>> GetDetailedLatestPosts()
        {
            return await _context.SMPosts
                .Take(100)
                .OrderByDescending(p => p.Pub_time)
                .Include(ps => ps.Publisher)
                .Include(ps => ps.ReferencePost)
                .ToListAsync();
        }

        public async Task<List<SMPostLike>> GetLikesByPostId(int id, int pageNumber, int pageSize)
        {
            return await _context.SMPostLikes
                .Where(pl => pl.PostId == id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}