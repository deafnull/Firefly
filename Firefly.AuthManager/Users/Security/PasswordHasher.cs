using System;
using System.Linq;
using System.Security.Cryptography;

namespace Firefly.AuthManager.Users.Security
{
    internal sealed class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 10000;
        
        public HashedPassword Hash(string password)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(
              password,
              SaltSize,
              Iterations,
              HashAlgorithmName.SHA512))
            {
                var key = Convert.ToBase64String(rfc2898.GetBytes(KeySize));
                var salt = Convert.ToBase64String(rfc2898.Salt);

                return new HashedPassword(key, salt);
            }
        }

        public bool IsValid(string hash, string salt, string password)
        {         
            using (var algorithm = new Rfc2898DeriveBytes(
              password,
              Convert.FromBase64String(salt),
              Iterations,
              HashAlgorithmName.SHA512))
            {
                var keyToCheck = algorithm.GetBytes(KeySize);
                var key = Convert.FromBase64String(hash);
                return keyToCheck.SequenceEqual(key);
            }
        }
    }
}
