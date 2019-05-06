using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;

namespace Firefly.AuthManager.IntegrationTests
{
    public class IntegrationTestFixture
    {
        private readonly ServiceProvider serviceProvider;

        public IntegrationTestFixture()
        {
            IServiceCollection services = new ServiceCollection();

            var retryPolicy = HttpPolicyExtensions
                  .HandleTransientHttpError()
                  .RetryAsync(3);

            var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(10);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("appsettings.json")
                .Build();

            var url = configuration.GetValue<string>("url");

            services.AddHttpClient<IApiClient, ApiClient>(c => c.BaseAddress = new Uri(url))
              .AddPolicyHandler(retryPolicy)
              .AddPolicyHandler(timeoutPolicy);

            serviceProvider = services.BuildServiceProvider();
        }

        public IApiClient CreateClient()
        {
            return serviceProvider.GetService<IApiClient>();
        }
    }
}
