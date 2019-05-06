namespace Firefly.AuthManager.Users.Authenticate
{
    internal class AuthenticateUserResponse : IAuthenticateUserResponse
    {
        public AuthenticateUserResponse(bool success, string message)
        {
            Success = success;
            Message = message ?? throw new System.ArgumentNullException(nameof(message));
        }

        public bool Success { get; }
        public string Message { get; }
    }
}