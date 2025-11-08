using Cozy_Chatter.Models;
using Microsoft.EntityFrameworkCore;

namespace Cozy_Chatter.Repositories
{
    public class SMPostRepository(ChatterContext context)
    {
        private readonly ChatterContext _context = context;

        public async Task<SMPost?> GetPostById(int id)
        {
            return await _context.SMPosts.FirstOrDefaultAsync(smp => smp.Id == id);
        }

        public async Task<List<SMPost>> GetDetailedPostsByUserId(int id)
        {
            var postsShort = _context.SMPosts.Where(p => p.UserId == id);
            return await postsShort
                .Include(ps => ps.Publisher)
                .Include(ps => ps.ReferencePost)
                .ToListAsync(); 
        } 

        //Select only TOP 100
        public async Task<List<SMPost>> GetDetailedLatestPosts()
        {
            var postsShort = _context.SMPosts.OrderByDescending(p => p.Pub_time).Take(100);
            return await postsShort
                .Include(ps => ps.Publisher)
                .Include(ps => ps.ReferencePost)
                .ToListAsync();
        }

        public async Task<List<SMPostLike>> GetLikesByPostId(int id)
        {
            return await _context.SMPostLikes.Where(pl => pl.PostId == id).ToListAsync();
        }
    }
}