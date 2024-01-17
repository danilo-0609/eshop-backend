using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.Wishes;

namespace Shopping.Infrastructure.Domain.Wishes;

internal sealed class WishConfiguration : IEntityTypeConfiguration<Wish>
{
    public void Configure(EntityTypeBuilder<Wish> builder)
    {
        builder.ToTable("Wishes", "shopping");

        builder.HasKey(x => x.Id);

        builder.Property(w => w.Id)
            .HasColumnName("WishId")
            .HasConversion(
            id => id.Value,
            value => WishId.Create(value))
            .ValueGeneratedNever();

        builder.Property(x => x.CustomerId)
            .HasColumnName("CustomerId");

        builder.HasMany(x => x.Items)
            .WithMany()
            .UsingEntity<WishItem>();

        builder.Property(x => x.Name)
            .HasColumnName("Name");

        builder.Property(x => x.IsPrivate)
            .HasColumnName("IsPrivate");

        builder.Property(x => x.CreatedOn)
            .HasColumnName("CreatedOn");
    }
}
