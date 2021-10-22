using Microsoft.EntityFrameworkCore;
using ServiceBusExample.Application.Common;
using ServiceBusExample.Domain.Entities;
using System.Reflection;

namespace ServiceBusExample.Infrastructure.Persistance
{
    public class MasstransitExampleDbContext : DbContext, IMasstransitExampleDbContext
    {
        public MasstransitExampleDbContext(DbContextOptions options) : base(options)
        {
        }

        public Category Category { get; set; }
        public Article Article { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}