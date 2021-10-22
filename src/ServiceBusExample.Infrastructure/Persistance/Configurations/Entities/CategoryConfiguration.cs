using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceBusExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusExample.Infrastructure.Persistance.Configurations.Entities
{
   public  class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(k => k.Id);
            builder.Property(t => t.Id).UseIdentityColumn(1, 1);
            builder.Property(p => p.Name).HasMaxLength(500).IsRequired(); 

        }
    }
}
