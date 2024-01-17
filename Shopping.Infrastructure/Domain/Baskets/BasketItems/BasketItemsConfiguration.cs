using Microsoft.EntityFrameworkCore;
using Shopping.Domain.Basket;

namespace Shopping.Infrastructure.Domain.Baskets.BasketsItems;

internal sealed class BasketItemsConfiguration : IEntityTypeConfiguration<BasketItems>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BasketItems> builder)
    {
        builder.ToTable("BasketItems", "shopping");

        builder.HasKey(k => new { k.BasketId, k.ItemId });

        builder.Property(p => p.BasketId)
            .HasColumnName("BasketId")
            .ValueGeneratedNever();

        builder.Property(p => p.ItemId)
            .HasColumnName("ItemId")
            .ValueGeneratedNever();
    }
}
