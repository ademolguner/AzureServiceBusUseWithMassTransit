using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceBusExample.Application.Common;
using ServiceBusExample.Infrastructure.Persistance.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusExample.Infrastructure.Persistance
{
    

    public class MasstransitExampleDbContextFactory : IMasstransitExampleDbContextFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public MasstransitExampleDbContextFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IMasstransitExampleDbContext CreateDbContext()
        {
            //var scope = _serviceProvider.CreateScope();
            var options = new DbContextOptionsBuilder();
            options.SetSqlServerOptions(_serviceProvider);
            return new MasstransitExampleDbContext(options.Options
                //,scope.ServiceProvider.GetRequiredService<ICurrentUserProvider>(),
                //scope.ServiceProvider.GetRequiredService<IDateTimeProvider>()
                );
        }
    }
}
