using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.Payments;

namespace Shopping.Infrastructure.Domain.Payments;

internal sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments", "shopping");

        builder.HasKey(k => k.Id);

        builder.Property(d => d.Id)
            .HasColumnName("PaymentId")
            .HasConversion(
            id => id.Value,
            value => PaymentId.Create(value))
            .ValueGeneratedNever();

        builder.Property(x => x.PayerId)
            .HasColumnName("PayerId");

        builder.Property(x => x.OrderId)
            .HasColumnName("OrderId");

        builder.Property(x => x.MoneyAmount)
            .HasColumnName("MoneyAmount");

        builder.Property(x => x.PayedOn)
            .HasColumnName("PayedOn");
    }
}
