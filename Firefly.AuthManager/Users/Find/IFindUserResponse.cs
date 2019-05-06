namespace Firefly.AuthManager.Users.Find
{
    public interface IFindUserResponse
    {
        bool Success { get; }
        string Message { get; }
    }
}