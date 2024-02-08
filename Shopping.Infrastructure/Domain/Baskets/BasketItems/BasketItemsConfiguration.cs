using Microsoft.EntityFrameworkCore;
using Shopping.Domain.Basket;

namespace Shopping.Infrastructure.Domain.Baskets.BasketsItems;

internal sealed class BasketItemsConfiguration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BasketItem> builder)
    {
        builder.ToTable("BasketItems", "shopping");

        builder.HasKey(k => new { k.BasketId, k.ItemId });

        builder.Property(p => p.BasketId)
            .HasColumnName("BasketId");

        builder.Property(p => p.ItemId)
            .HasColumnName("ItemId");

        builder.Property(p => p.AmountPerItem)
            .HasColumnName("AmountPerItem");
    }
}
