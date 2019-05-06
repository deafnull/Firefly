using System;
using System.Collections.Generic;

namespace Firefly.AuthManager.Users.Store
{
    internal class InvalidUsernameOrPasswordException : Exception
    {
        public InvalidUsernameOrPasswordException(IEnumerable<string> errors)
             : base(string.Join('\n', errors))
        {
            
        }
    }
}