using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Firefly.AuthManager.IntegrationTests
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient httpClient;

        public ApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<StoreUserResponse> Store(StoreUserRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var serializedUser = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(serializedUser, Encoding.UTF8, "application/json");
            var httpRequest = new HttpRequestMessage(HttpMethod.Put, "users")
            {
                Content = httpContent
            };

            var httpResponse = await httpClient.SendAsync(httpRequest);

            httpResponse.EnsureSuccessStatusCode();

            var serializedResponse = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<StoreUserResponse>(serializedResponse);
        }

        public async Task<FindUserResponse> Find(string username)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"users?username={username}");
            var httpResponse = await httpClient.SendAsync(httpRequest);

            var serializedResponse = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FindUserResponse>(serializedResponse);
        }

        public async Task<AuthenticateUserResponse> Authenticate(AuthenticateUserRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var serializedUser = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(serializedUser, Encoding.UTF8, "application/json");
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "users/auth")
            {
                Content = httpContent
            };

            var httpResponse = await httpClient.SendAsync(httpRequest);

            var serializedResponse = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AuthenticateUserResponse>(serializedResponse);
        }
    }
}
