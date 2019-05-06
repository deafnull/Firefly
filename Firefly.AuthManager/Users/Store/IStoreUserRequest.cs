namespace Firefly.AuthManager.Users.Store
{
    public interface IStoreUserRequest
    {
        string Username { get; }
        string Password { get; }
    }
}