using Cozy_Chatter.Data;
using Cozy_Chatter.Hashing;
using Microsoft.EntityFrameworkCore;

namespace Cozy_Chatter.Repositories
{
    public class CredentialRepository(ChatterContext context) : AbstractRepository(context)
    {
        public override async Task<int> GetCountAsync()
        {
            return await _context.Credentials.CountAsync();
        }
        public async Task<bool> CheckCredentialsExist(string login, string password)
        {
            var salt = await _context.Credentials
                .Where(cr => cr.Login == login)
                .Select(cr => cr.Salt)
                .FirstOrDefaultAsync();
            string hashedpass = PasswordHasher.HashPassword(password, salt);
            return await _context.Credentials.AnyAsync(cr => cr.Login == login && cr.Password == hashedpass);
        }
    }
}