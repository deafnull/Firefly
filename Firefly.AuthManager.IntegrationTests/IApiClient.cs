using System.Threading.Tasks;

namespace Firefly.AuthManager.IntegrationTests
{
    public interface IApiClient
    {
        Task<StoreUserResponse> Store(StoreUserRequest request);
        Task<FindUserResponse> Find(string username);
        Task<AuthenticateUserResponse> Authenticate(AuthenticateUserRequest request);
    }
}
