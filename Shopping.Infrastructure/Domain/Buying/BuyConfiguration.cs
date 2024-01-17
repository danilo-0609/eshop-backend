using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.Buying;
using Shopping.Domain.Items;

namespace Shopping.Infrastructure.Domain.Buying;

internal sealed class BuyConfiguration : IEntityTypeConfiguration<Buy>
{
    public void Configure(EntityTypeBuilder<Buy> builder)
    {
        builder.ToTable("Buys", "shopping");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("BuyId")
            .HasConversion(
            id => id.Value,
            value => BuyId.Create(value))
            .ValueGeneratedNever();

        builder.Property(x => x.BuyerId)
            .HasColumnName("BuyerId");

        builder.Property(x => x.ItemId)
            .HasColumnName("ItemId")
            .HasConversion(
            id => id.Value,
            value => ItemId.Create(value));

        builder.Property(x => x.AmountOfProducts)
            .HasColumnName("AmountOfProduct");

        builder.Property(x => x.UnitPrice)
            .HasColumnName("UnitPrice")
            .HasColumnType("decimal (18, 2)");

        builder.Property(x => x.UnitPrice)
            .HasColumnName("UnitPrice")
            .HasColumnType("decimal (18, 2)");

        builder.Property(x => x.OcurredOn)
            .HasColumnName("OcurredOn");
    }
}
