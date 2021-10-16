using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusExample.Infrastructure.Persistance.Configurations
{
    public static class SqlServerOptionConfigurationExtension
    {
        public static void SetSqlServerOptions(this DbContextOptionsBuilder options, IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            options.UseSqlServer(configuration.GetConnectionString("Default"),
                        b => b.MigrationsAssembly(typeof(MasstransitExampleDbContext).Assembly.FullName));

            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            options.UseLoggerFactory(loggerFactory);
            var hostEnvironment = serviceProvider.GetRequiredService<IHostEnvironment>();
            if (hostEnvironment.IsDevelopment())
            {
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            }
        }
    }
}
