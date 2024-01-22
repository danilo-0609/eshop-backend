using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.Items;
using Shopping.Domain.Orders;

namespace Shopping.Infrastructure.Domain.Orders;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders", "shopping");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("OrderId")
            .HasConversion(
            id => id.Value,
            value => OrderId.Create(value))
            .ValueGeneratedNever();

        builder.Property(x => x.CustomerId)
            .HasColumnName("CustomerId");

        builder.Property(x => x.ItemId)
            .HasColumnName("ItemId")
            .HasConversion(
            id => id.Value,
            value => ItemId.Create(value));

        builder.Property(x => x.AmountOfItems)
            .HasColumnName("AmountOfItems");

        builder.Property(x => x.TotalMoneyAmount)
            .HasColumnName("TotalMoneyAmount");

        builder.OwnsOne<OrderStatus>("OrderStatus", f =>
        {
            f.Property(g => g.Value).HasColumnName("OrderStatus");
        });

        builder.Property(x => x.PlacedOn)
            .HasColumnName("PlacedOn");

        builder.Property(x => x.ConfirmedOn)
            .HasColumnName("ConfirmedOn")
            .IsRequired(false);

        builder.Property(x => x.ExpiredOn)
            .HasColumnName("ExpireOn")
            .IsRequired(false);

        builder.Property(x => x.PayedOn)
            .HasColumnName("PayedOn")
            .IsRequired(false);

        builder.Property(x => x.CompletedOn)
            .HasColumnName("CompletedOn")
            .IsRequired(false);
    }
}
