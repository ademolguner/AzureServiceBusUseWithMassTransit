using System;
using Microsoft.Extensions.DependencyInjection;
using ServiceBusExample.Domain.Common.DI;

namespace ServiceBusExample.Api.StartupConfigurations
{
    public static class IocConfiguration
    {
        public static void RegisterAllDependencies(this IServiceCollection services)
        {  
            services.Scan(scan =>
            {
                scan.FromApplicationDependencies(a => a.GetName().Name.StartsWith("ServiceBusExample"))
                    .AddClasses(classes => classes.AssignableTo<ITransientDependency>())
                    .AsSelfWithInterfaces()
                    .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo<IScopedDependency>())
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime()
                    .AddClasses(classes => classes.AssignableTo<ISingletonDependency>())
                    .AsSelfWithInterfaces()
                    .WithSingletonLifetime();
            });
        }
    }
}
