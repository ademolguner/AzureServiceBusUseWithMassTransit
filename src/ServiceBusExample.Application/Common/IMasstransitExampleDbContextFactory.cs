using ServiceBusExample.Domain.Common.DI;

namespace ServiceBusExample.Application.Common
{
    public interface IMasstransitExampleDbContextFactory : IScopedDependency
    {
        IMasstransitExampleDbContext CreateDbContext();
    }
}