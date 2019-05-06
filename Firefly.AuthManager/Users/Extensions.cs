using Firefly.AuthManager.Users.Security;
using Firefly.AuthManager.Users.Store;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Firefly.AuthManager.Users
{
    public static class Extensions
    {
        public static IServiceCollection AddUsers<T>(this IServiceCollection services, ILogger logger,
            IConfiguration configuration) where T : class, IUsernamePasswordPolicyValidator
        {
            services.AddSingleton(logger);

            services.AddMediatR();
            services.Configure<DatabaseSettings>(configuration.GetSection("Database"));
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<IUsernamePasswordPolicyValidator, T>();
            services.AddSingleton<IFireflyAuthDatabase, FireflyAuthDatabase>();
            services.AddTransient<IUsersRepository, UsersRepository>();

            return services;
        }
    }
}
