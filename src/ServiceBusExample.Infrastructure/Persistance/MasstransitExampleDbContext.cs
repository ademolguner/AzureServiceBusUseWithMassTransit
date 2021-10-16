using Microsoft.EntityFrameworkCore;
using ServiceBusExample.Application.Common;

namespace ServiceBusExample.Infrastructure.Persistance
{
    public class MasstransitExampleDbContext : DbContext, IMasstransitExampleDbContext
    {
        public MasstransitExampleDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}