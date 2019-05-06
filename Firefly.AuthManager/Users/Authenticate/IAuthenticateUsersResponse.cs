namespace Firefly.AuthManager.Users.Authenticate
{
    public interface IAuthenticateUserResponse
    {
        bool Success { get; }
        string Message { get; }
    }
}