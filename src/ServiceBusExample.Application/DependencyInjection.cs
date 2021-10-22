using Arch.EntityFrameworkCore.UnitOfWork;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ServiceBusExample.Application.Repositories;
using System.Linq;
using System.Reflection;

namespace ServiceBusExample.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            var repos = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t =>
                   t.BaseType != null
                && t.BaseType.IsGenericType
                && t.BaseType.GetGenericTypeDefinition() == typeof(Repository<>))
                .ToList();
            foreach (var item in repos)
            {
                foreach (var interf in item.GetInterfaces())
                {
                    services.AddScoped(interf, item);
                }
            }
            services.AddScoped<IRepositoryContext, RepositoryContext>();

            return services;
        }
    }
}
