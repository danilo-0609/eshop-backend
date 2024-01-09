using Catalog.Domain.Products;
using Catalog.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Domain.Sales;
internal sealed class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales", "catalog");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => SaleId.Create(value))
            .ValueGeneratedNever()
            .HasColumnName("SaleId");
        
        builder.Property(p => p.ProductId)
            .HasConversion(
                id => id.Value,
                value => ProductId.Create(value))
            .HasColumnName("ProductId");

        builder.Property(p => p.AmountOfProducts)
            .HasColumnName("AmountOfProducts");

        builder.Property(p => p.UnitPrice)
            .HasColumnName("UnitPrice")
            .HasColumnType("decimal(18, 2)");

        builder.Property(p => p.UserId)
            .HasColumnName("UserId");

        builder.Property(p => p.CreatedDateTime)
            .HasColumnName("CreatedDateTime");
    }
}