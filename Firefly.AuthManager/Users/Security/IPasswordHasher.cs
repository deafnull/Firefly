namespace Firefly.AuthManager.Users.Security
{
    public interface IPasswordHasher
    {
        HashedPassword Hash(string password);
        bool IsValid(string hash, string salt, string password);
    }
}
