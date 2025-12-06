using Cozy_Chatter.Data;
using Microsoft.EntityFrameworkCore;

namespace Cozy_Chatter.Repositories
{
    public class ProfilePictureRepository(ChatterContext context) : 
        AbstractRepository(context), IProfilePictureRepository
    {
        public override async Task<int> GetCountAsync()
        {
            return await _context.Pfpictures.CountAsync();
        }
        public async Task<List<int>> GetProfilePicturesByUserId(int id, int pageNumber, int pageSize)
        {
            return await _context.Pfpictures
                .Where(pfp => pfp.UserId == id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(pfp => pfp.PictureId)
                .ToListAsync();
        }
    }

    public interface IProfilePictureRepository : IRepository
    {
        Task<List<int>> GetProfilePicturesByUserId(int id, int pageNumber, int pageSize);
    }
}