namespace Firefly.AuthManager.Users
{
    internal class StoredUser : IUser
    {
        public string Username { get; set; }

        public string Hash { get; set; }

        public string Salt { get; set; }
    }
}
