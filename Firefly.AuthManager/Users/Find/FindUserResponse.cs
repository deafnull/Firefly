namespace Firefly.AuthManager.Users.Find
{
    internal class FindUserResponse : IFindUserResponse
    {
        public FindUserResponse(bool success, string message)
        {
            Success = success;
            Message = message ?? throw new System.ArgumentNullException(nameof(message));
        }

        public bool Success { get; }
        public string Message { get; }
    }
}