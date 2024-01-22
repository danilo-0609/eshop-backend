using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.Basket;

namespace Shopping.Infrastructure.Domain.Baskets;

internal class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.ToTable("baskets", "shopping");

        builder.HasKey(k => k.Id);

        builder.Property(k => k.Id)
            .HasColumnName("BasketId")
            .HasConversion(
                id => id.Value,
                value => BasketId.Create(value))
            .ValueGeneratedNever();

        builder.Property(p => p.CustomerId)
            .HasColumnName("CustomerId")
            .ValueGeneratedNever();

        builder.HasMany(r => r.ItemIds)
            .WithMany()
            .UsingEntity<BasketItem>();

        builder.Property(d => d.AmountOfProducts)
            .HasColumnName("AmountOfProducts");

        builder.Property(d => d.TotalAmount)
            .HasColumnName("TotalAmount")
            .HasColumnType("decimal (18, 2)");

        builder.Property(d => d.CreatedOn)
            .HasColumnName("CreatedOn");

        builder.Property(d => d.UpdatedOn)
            .HasColumnName("UpdatedOn")
            .IsRequired(false);
    }
}

