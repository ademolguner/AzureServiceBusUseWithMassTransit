using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceBusExample.Domain.Entities;

namespace ServiceBusExample.Infrastructure.Persistance.Configurations.Entities
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles");
            builder.HasKey(k => k.Id);
            builder.Property(t => t.Id).UseIdentityColumn(1, 1);
            builder.Property(p => p.Title).HasMaxLength(500).IsRequired();
                builder.Property(p=> p.IsIndex).HasColumnType("bit");
        }
    }
}