namespace Firefly.AuthManager.Users.Store
{
    public interface IStoreUserResponse
    {
        bool Success { get; }
        string Message { get; }
    }
}