using Catalog.Domain.Products;
using Catalog.Domain.Ratings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Domain.Ratings;
internal sealed class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.ToTable("Ratings", "catalog");

        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => RatingId.Create(value))
            .ValueGeneratedNever()
            .HasColumnName("RatingId");
        
        builder.Property(p => p.Feedback)
            .HasColumnName("Feedback");

        builder.Property(p => p.UserId)
            .HasColumnName("UserId");
        
        builder.Property(p => p.ProductId)
            .HasConversion(
                id => id.Value,
                value => ProductId.Create(value))
            .HasColumnName("ProductId");
        
        builder.Property<double>(p => p.Rate)
            .HasColumnName("Rate");

        builder.Property(p => p.CreatedDateTime)
            .HasColumnName("CreatedDateTime");
        
        builder.Property(p => p.UpdatedDateTime)
            .HasColumnName("UpdatedDateTime");
    }
}