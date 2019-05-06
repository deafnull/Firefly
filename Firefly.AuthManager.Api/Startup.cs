using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Firefly.AuthManager.Users;
using Swashbuckle.AspNetCore.Swagger;
using Firefly.AuthManager.Users.Store;

namespace Firefly.AuthManager.Api
{
    public class Startup
    {
        public Startup()
        {
            var configuration = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json") 
                  .AddEnvironmentVariables()
                  .Build();
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .WriteTo.Console()
                    .CreateLogger();

            Log.Logger.Information("Starting Firefly.AuthManager.Api");
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver()
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    };
                })
                .AddFluentValidation()                
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("doc", new Info { Title = "Firefly.AuthManager.Api" });
            });
            services.AddOptions();
            services.AddUsers<UsernamePasswordPolicyValidator>(Log.Logger, Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("../swagger/doc/swagger.json", "Firefly.AuthManager.Api");
            });
        }
    }
}
