using Cozy_Chatter.Data;
using Cozy_Chatter.Hashing;
using Cozy_Chatter.Models;
using Microsoft.EntityFrameworkCore;

namespace Cozy_Chatter.Repositories
{
    public class CredentialRepository(ChatterContext context) : AbstractRepository(context), ICredentialRepository
    {
        public override async Task<int> GetCountAsync()
        {
            return await _context.Credentials.CountAsync();
        }
        public async Task<bool> CheckCredentialsExist(string login, string password)
        {
            var creds = _context.Credentials.Where(cr => cr.Login == login);
            var salt = await creds
                .Select(cr => cr.Salt)
                .FirstOrDefaultAsync();
            return await creds.AnyAsync(cr => cr.Password == PasswordHasher.HashPassword(password, salt));
        }   
        public async Task<User?> ValidateUserAsync(string login, string password)
        {
            var creds = _context.Credentials.Where(cr => cr.Login == login);
            var salt = await creds
                .Select(cr => cr.Salt)
                .FirstOrDefaultAsync();
            return await creds
                .Include(c => c.User)
                .Where(cr => cr.Password == PasswordHasher.HashPassword(password, salt))
                .Select(cr => cr.User)
                .FirstOrDefaultAsync();
        }
    }

    public interface ICredentialRepository : IRepository
    {
        Task<bool> CheckCredentialsExist(string login, string password);
        Task<User?> ValidateUserAsync(string login, string password);
    }
}