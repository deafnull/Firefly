using Firefly.AuthManager.Users.Security;
using System;

namespace Firefly.AuthManager.Users.Store
{
    internal class UserToStore : IUser
    {
        private readonly StoreUserCommand command;

        public UserToStore(StoreUserCommand command, HashedPassword hashedPassword)
        {
            if (hashedPassword == null)
            {
                throw new ArgumentNullException(nameof(hashedPassword));
            }

            this.command = command ?? throw new ArgumentNullException(nameof(command));

            Username = command.Username ?? throw new ArgumentNullException(nameof(Username));
            Hash = hashedPassword.Hash ?? throw new ArgumentNullException(nameof(Hash));
            Salt = hashedPassword.Salt ?? throw new ArgumentNullException(nameof(Salt));
        }

        public string Username { get; }

        public string Hash { get; }

        public string Salt { get; }
    }
}
