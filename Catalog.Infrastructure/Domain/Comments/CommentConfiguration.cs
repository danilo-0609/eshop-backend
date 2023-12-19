using System.Drawing;
using Catalog.Domain.Comments;
using Catalog.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Domain.Comments;
internal sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments", "catalog");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("CommentId")
            .HasConversion(
                id => id.Value,
                value => CommentId.Create(value))
            .ValueGeneratedNever();

        builder.Property(p => p.UserId)
            .HasColumnName("UserId");

        builder.Property(p => p.ProductId)
            .HasConversion(
                id => id.Value,
                value => ProductId.Create(value))
            .HasColumnName("ProductId");

        builder.Property(p => p.Content)
            .HasColumnName("Content")
            .HasMaxLength(3000);

        builder.Property(p => p.IsActive)
            .HasColumnName("IsActive");

        builder.Property(p => p.CreatedDateTime)
            .HasColumnName("CreatedDateTime");

        builder.Property(p => p.UpdatedDateTime)
            .HasColumnName("UpdatedDateTime")
            .IsRequired(false);
    }
}