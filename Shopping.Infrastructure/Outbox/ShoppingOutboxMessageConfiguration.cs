using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shopping.Infrastructure.Outbox;

internal sealed class ShoppingOutboxMessageConfiguration : IEntityTypeConfiguration<ShoppingOutboxMessage>
{
    public void Configure(EntityTypeBuilder<ShoppingOutboxMessage> builder)
    {
        builder.ToTable("ShoppingOutboxMessages", "shopping");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .ValueGeneratedNever();

        builder.Property(x => x.Type).HasColumnName("Type");

        builder.Property(x => x.Content).HasColumnName("Content");

        builder.Property(x => x.OcurredOnUtc).HasColumnName("OcurredOnUtc");

        builder.Property(x => x.ProcessedOnUtc).HasColumnName("ProcessedOnUtc").IsRequired(false);

        builder.Property(x => x.Error).HasColumnName("Error");
    }
}

