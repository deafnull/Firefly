using Firefly.AuthManager.Users.Store;

namespace Firefly.AuthManager.Api.Users
{
    public class StoreUserRequest : IStoreUserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}