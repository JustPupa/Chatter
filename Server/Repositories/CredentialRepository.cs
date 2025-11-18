using Cozy_Chatter.Hashing;
using Microsoft.EntityFrameworkCore;

namespace Cozy_Chatter.Repositories
{
    public class CredentialRepository(ChatterContext context)
    {
        private readonly ChatterContext _context = context;
        public async Task<bool> CheckCredentialsExist(string login, string password)
        {
            string hashedpass = PasswordHasher.HashPassword(password);
            return await _context.Credentials
                .AnyAsync(cr => cr.Login == login && cr.Password == hashedpass);
        }
    }
}