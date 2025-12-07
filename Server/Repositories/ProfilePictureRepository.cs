using Cozy_Chatter.Data;
using Cozy_Chatter.Models;
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
        public async Task<List<Pfpicture>> GetProfilePicturesByUserId(int id, int pageNumber, int pageSize)
        {
            return await _context.Pfpictures
                .Where(pfp => pfp.UserId == id)
                .OrderBy(pfp => pfp.PictureId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }

    public interface IProfilePictureRepository : IRepository
    {
        Task<List<Pfpicture>> GetProfilePicturesByUserId(int id, int pageNumber, int pageSize);
    }
}