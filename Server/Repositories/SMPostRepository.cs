using Cozy_Chatter.DTOs;
using Cozy_Chatter.Models;
using Microsoft.EntityFrameworkCore;

namespace Cozy_Chatter.Repositories
{
    public class SMPostRepository
    {
        public static async Task<SMPost?> GetPostById(int id)
        {
            using var context = new ChatterContext();
            return await context.SMPosts.FirstOrDefaultAsync(smp => smp.Id == id);
        }

        public static async Task<List<SMPostFull>?> GetDetailedPostsByUserId(int id)
        {
            using var context = new ChatterContext();
            var postsShort = context.SMPosts.Where(p => p.UserId == id);
            return await postsShort.Select(ps => SMPost.ToDetailed(ps)).ToListAsync();
        } 

        //Select only TOP 100
        public static async Task<List<SMPostFull>?> GetDetailedLatestPosts()
        {
            using var context = new ChatterContext();
            var postsShort = context.SMPosts.OrderByDescending(p => p.Pub_time).Take(100);
            return await postsShort.Select(ps => SMPost.ToDetailed(ps)).ToListAsync();
        }

        public static async Task<List<SMPostLike>?> GetLikesByPostId(int id)
        {
            using var context = new ChatterContext();
            return await context.SMPostLikes.Where(pl => pl.PostId == id).ToListAsync();
        }
    }
}