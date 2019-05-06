using System.Threading.Tasks;

namespace Firefly.AuthManager.Users
{
    internal interface IUsersRepository
    {
        Task Insert(IUser user);

        Task<IUser> Get(string userName);
    }
}
