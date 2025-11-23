using Isopoh.Cryptography.Argon2;
using System.Security.Cryptography;
using System.Text;

namespace Cozy_Chatter.Hashing
{
    public class PasswordHasher
    {
        public static string HashPassword(string password, byte[]? userSalt = null)
        {
            byte[] salt = userSalt??RandomNumberGenerator.GetBytes(16);
            var config = new Argon2Config
            {
                Type = Argon2Type.DataIndependentAddressing,
                Version = Argon2Version.Nineteen,
                TimeCost = 4,
                MemoryCost = 1024 * 64,
                Lanes = 4,
                Threads = 4,
                Password = Encoding.UTF8.GetBytes(password),
                Salt = salt,
                HashLength = 32,
            };
            using var argon2 = new Argon2(config);
            var hash = argon2.Hash();
            return config.EncodeString(hash.Buffer);
        }
        public static bool VerifyPassword(string password, string encodedHash)
        {
            return Argon2.Verify(encodedHash, password);
        }
    }
}