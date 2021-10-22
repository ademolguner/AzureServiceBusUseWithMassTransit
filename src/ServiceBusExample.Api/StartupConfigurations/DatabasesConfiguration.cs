using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBusExample.Api.Common;
using ServiceBusExample.Application.Common;
using ServiceBusExample.Infrastructure.Persistance;
using ServiceBusExample.Infrastructure.Persistance.Configurations;
using System;

namespace ServiceBusExample.Api.StartupConfigurations
{
    public static class DatabasesConfiguration
    {
        public static IServiceCollection CheckAndConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigurationDbCheckOption(services, configuration);
            services.AddUnitOfWork<MasstransitExampleDbContext>();
            services.AddScoped<IMasstransitExampleDbContext>(provider => provider.GetService<MasstransitExampleDbContext>());

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
                var provider = services.BuildServiceProvider();
                services.AddDbContext<MasstransitExampleDbContext>((options) =>
                {
                    options.SetSqlServerOptions(provider);
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