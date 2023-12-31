using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Outbox;

internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasKey(k => k.Id);

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
