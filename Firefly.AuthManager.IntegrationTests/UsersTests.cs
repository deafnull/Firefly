using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Firefly.AuthManager.IntegrationTests
{
    public class UsersTests : IClassFixture<IntegrationTestFixture>
    {
        private readonly IApiClient client;

        public UsersTests(IntegrationTestFixture fixture)
        {
            client = fixture.CreateClient();
        }

        [Fact]
        public async Task CanStoreUser()
        {
            var request = GenerateRequest();

            var result = await client.Store(request);

            result.ShouldNotBeNull();
            result.Success.ShouldBeTrue();
        }

        private StoreUserRequest GenerateRequest(string username = null)
        {
            return new StoreUserRequest()
            {
                Username = username ?? Guid.NewGuid().ToString().Replace("-", string.Empty),
                Password = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)
            };
        }

        [Fact]
        public async Task CanFindUser()
        {
            var request = GenerateRequest();
            var storeResult = await client.Store(request);

            var result = await client.Find(request.Username);

            result.ShouldNotBeNull();
            result.Success.ShouldBeTrue();
        }


        [Fact]
        public async Task GivenUserDoesNotExistCannotFindUser()
        {
            var userId = Guid.NewGuid().ToString();

            var result = await client.Find(userId);

            result.ShouldNotBeNull();
            result.Success.ShouldBeFalse();
        }

        [Fact]
        public async Task CanAuthenticateUser()
        {
            var request = GenerateRequest();
            var storeResult = await client.Store(request);
            var authRequest = GenerateRequest(request.Username, request.Password);

            var result = await client.Authenticate(authRequest);

            result.ShouldNotBeNull();
            result.Success.ShouldBeTrue();
        }

        private AuthenticateUserRequest GenerateRequest(string username, string password)
        {
            return new AuthenticateUserRequest()
            {
                Username = username,
                Password = password
            };
        }

        [Fact]
        public async Task GivenInvalidPasswordDoesNotAuthenticate()
        {
            var request = GenerateRequest();
            var storeResult = await client.Store(request);
            var authRequest = GenerateRequest(request.Username, Guid.NewGuid().ToString());

            var result = await client.Authenticate(authRequest);

            result.ShouldNotBeNull();
            result.Success.ShouldBeFalse();
        }
    }
}
