using System; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBusExample.Api.Common;
using ServiceBusExample.Infrastructure.Persistance;
using Arch.EntityFrameworkCore.UnitOfWork;
using ServiceBusExample.Infrastructure.Persistance.Configurations;

namespace ServiceBusExample.Api.StartupConfigurations
{
    public static class DatabasesConfiguration
    {
        public static IServiceCollection CheckAndConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigurationDbCheckOption(services, configuration);
            services.AddUnitOfWork<MasstransitExampleDbContext>();
            services.AddScoped(provider => provider.GetService<MasstransitExampleDbContext>());
            return services;
        }

        private static void ConfigurationDbCheckOption(IServiceCollection services, IConfiguration configuration)
        {
            SetHealthCheckVersion(services);

            if (configuration.GetValue<bool>("UseInMemoryDatabase")
                || string.IsNullOrEmpty(configuration.GetConnectionString("Default")))
            {
                services.AddDbContext<MasstransitExampleDbContext>(options =>
                {
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                });
            }
            else
            {
                
                services.AddDbContext<MasstransitExampleDbContext>((provider, options) =>
                {
                    //options.SetSqlServerOptions(provider);
                });
            }
        }

        private static void SetHealthCheckVersion(IServiceCollection services)
        {
            services
                .AddHealthChecks()
                .AddCheck<VersionHealthCheck>("Version", tags: new[] { "Version" });
        }
    }
}
