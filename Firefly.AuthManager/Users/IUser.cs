using System;

namespace Firefly.AuthManager.Users
{
    internal interface IUser
    {
        string Username { get; }
        string Hash { get; }
        string Salt { get; }
    }
}
