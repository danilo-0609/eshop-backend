using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Outbox;

internal sealed class CatalogOutboxMessageConfiguration : IEntityTypeConfiguration<CatalogOutboxMessage>
{
    public void Configure(EntityTypeBuilder<CatalogOutboxMessage> builder)
    {
        builder.HasKey(k => k.Id);

        builder.ToTable("CatalogOutboxMessage", "catalog");

        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        builder.Property(p => p.Type)
            .HasColumnName("Type");

        builder.Property(p => p.Content)
            .HasColumnName("Content");

        builder.Property(p => p.OcurredOnUtc)
            .HasColumnName("OcurredOnUtc");

        builder.Property(p => p.ProcessedOnUtc)
            .HasColumnName("ProcessedOnUtc");

        builder.Property(p => p.Error)
            .HasColumnName("Error");
    }
}
