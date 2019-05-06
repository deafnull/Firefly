using System;

namespace Firefly.AuthManager.Users.Store
{
    internal class StoreUserResponse : IStoreUserResponse
    {
        public StoreUserResponse(bool success, string message)
        {
            Success = success;
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public bool Success { get; }
        public string Message { get; }
    }
}