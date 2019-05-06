using System;

namespace Firefly.AuthManager.Users.Security
{
    public class HashedPassword
    {
        public HashedPassword(string hash, string salt)
        {
            Hash = hash ?? throw new ArgumentNullException(nameof(hash));
            Salt = salt ?? throw new ArgumentNullException(nameof(salt));
        }

        public string Hash { get; }
        public string Salt { get; }
    }
}