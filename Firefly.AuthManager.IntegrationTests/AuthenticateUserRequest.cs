using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Firefly.AuthManager.IntegrationTests
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class AuthenticateUserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
