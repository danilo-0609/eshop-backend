using Catalog.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configuration;
internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products", "catalog");
    
        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => ProductId.Create(value))
            .ValueGeneratedNever()
            .HasColumnName("ProductId");

        builder.Property(p => p.SellerId)
            .HasColumnName("SellerId");

        builder.Property(p => p.Name)
            .HasColumnName("Name")
            .HasMaxLength(300);

        builder.Property(p => p.Price)
            .HasColumnName("Price");

        builder.Property(p => p.Description)
            .HasColumnName("Description")
            .HasMaxLength(9000);

        builder.Property(p => p.Size)
            .HasColumnName("Size")
            .HasMaxLength(100);

        builder.Property(p => p.Color)
            .HasColumnName("Color")
            .HasMaxLength(100);

        builder.OwnsOne(p => p.ProductType, b =>
        {
            b.Property(p => p.Value).HasColumnName("ProductTypeValue");
        });
        
        builder.OwnsMany<Tag>("Tags", y => 
        {
            y.WithOwner().HasForeignKey("ProductId");
            y.ToTable("Tags", "catalog");
            
            y.Property(y => y.Value)
                .HasColumnName("Value");
        });

        builder.Property(p => p.InStock)
            .HasColumnName("InStock");

        builder.Property(p => p.IsActive)
            .HasColumnName("IsActive");
        
        builder.Property(p => p.CreatedDateTime)
            .HasColumnName("CreatedDateTime");

        builder.Property(p => p.UpdatedDateTime)
            .HasColumnName("UpdatedDateTime")
            .IsRequired(false);

        builder.Property(p => p.ExpiredDateTime)
            .HasColumnName("ExpiredDateTime")
            .IsRequired(false);
    }
}
