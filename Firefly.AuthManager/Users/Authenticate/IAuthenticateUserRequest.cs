namespace Firefly.AuthManager.Users.Authenticate
{
    public interface IAuthenticateUserRequest
    {
        string Username { get; }
        string Password { get; }
    }
}