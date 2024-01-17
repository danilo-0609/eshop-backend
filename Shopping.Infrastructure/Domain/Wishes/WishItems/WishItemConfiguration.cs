using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.Wishes;

namespace Shopping.Infrastructure.Domain.Wishes.WishItems;

internal sealed class WishItemConfiguration : IEntityTypeConfiguration<WishItem>
{
    public void Configure(EntityTypeBuilder<WishItem> builder)
    {
        builder.ToTable("WishItems", "shopping");

        builder.HasKey(k => new { k.WishId, k.ItemId });

        builder.Property(x => x.WishId).HasColumnName("WishId");

        builder.Property(x => x.ItemId).HasColumnName("ItemId");
    }
}