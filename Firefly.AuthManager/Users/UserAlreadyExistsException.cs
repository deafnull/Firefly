using System;

namespace Firefly.AuthManager.Users
{
    internal class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string username) 
            : base($"User {username} already exists")
        {
        }
    }
}