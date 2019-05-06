using Firefly.AuthManager.Users.Authenticate;

namespace Firefly.AuthManager.Api.Users
{
    public class AuthenticateUserRequest : IAuthenticateUserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
