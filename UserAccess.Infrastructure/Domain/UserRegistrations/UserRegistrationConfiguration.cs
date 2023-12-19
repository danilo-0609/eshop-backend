using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAccess.Domain.Common;
using UserAccess.Domain.UserRegistrations;

namespace UserAccess.Infrastructure.Domain.UserRegistrations;

internal sealed class UserRegistrationConfiguration : IEntityTypeConfiguration<UserRegistration>
{
    public void Configure(EntityTypeBuilder<UserRegistration> builder)
    {
        builder.ToTable("UserRegistrations", "users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                userRegistrationId => userRegistrationId.Value,
                value => UserRegistrationId.Create(value))
            .ValueGeneratedNever()
            .HasColumnName("UserRegistrationId");

        builder.Property(x => x.Login)
            .HasColumnName("Login")
            .HasMaxLength(70);

        builder.OwnsOne<Password>("Password", p =>
        {
            p.Property(p => p.Value)
             .HasColumnName("Password")
             .HasMaxLength(25);
        });

        builder.Property(x => x.Email)
            .HasColumnName("Email");

        builder.Property(x => x.FirstName)
            .HasColumnName("FirstName")
            .HasMaxLength(70);

        builder.Property(x => x.LastName)
            .HasColumnName("LastName")
            .HasMaxLength(70);

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasMaxLength(155);

        builder.Property(x => x.Address)
            .HasColumnName("Address")
            .HasMaxLength(100);

        builder.Property(x => x.RegisteredDate)
            .HasColumnName("RegisteredDate");

        builder.OwnsOne<UserRegistrationStatus>("Status", s =>
        {
            s.Property(x => x.Value).HasColumnName("StatusCode");
        });

        builder.Property(x => x.ConfirmedDate)
            .HasColumnName("ConfirmedDate");
    }
}
