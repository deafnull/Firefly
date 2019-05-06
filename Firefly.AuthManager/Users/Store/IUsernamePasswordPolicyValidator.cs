namespace Firefly.AuthManager.Users.Store
{
    public interface IUsernamePasswordPolicyValidator
    {
        void ThrowExceptionIfInvalid(StoreUserCommand command);
    }
}
