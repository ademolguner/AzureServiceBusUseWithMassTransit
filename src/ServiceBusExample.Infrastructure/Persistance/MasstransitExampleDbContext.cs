using Microsoft.EntityFrameworkCore;
using ServiceBusExample.Application.Common;
using ServiceBusExample.Domain.Entities;

namespace ServiceBusExample.Infrastructure.Persistance
{
    public class MasstransitExampleDbContext : DbContext, IMasstransitExampleDbContext
    {
        public MasstransitExampleDbContext(DbContextOptions options) : base(options)
        {
        }

        public Category Category { get; set; }
        public Article Article { get; set; }
    }
}