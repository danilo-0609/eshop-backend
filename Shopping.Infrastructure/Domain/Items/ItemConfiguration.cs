using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.Items;

namespace Shopping.Infrastructure.Domain.Items;

internal sealed class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Items", "shopping");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("ItemId")
            .HasConversion(
            id => id.Value,
            value => ItemId.Create(value))
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .HasColumnName("Name");

        builder.Property(x => x.SellerId)
            .HasColumnName("SellerId");

        builder.Property(x => x.Price)
            .HasColumnName("Price")
            .HasColumnType("decimal(18, 2)");

        builder.Property(x => x.InStock)
            .HasColumnName("InStock");

        builder.OwnsOne<StockStatus>("StockStatus", t =>
        {
            t.Property(x => x.Value).HasColumnName("StockStatus");
        });


        builder.Property(x => x.CreatedOn)
            .HasColumnName("CreatedOn");

        builder.Property(x => x.UpdatedOn)
            .HasColumnName("UpdatedOn")
            .IsRequired(false);
    }
}