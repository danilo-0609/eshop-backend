using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAccess.Domain.Common;
using UserAccess.Domain.Users;

namespace UserAccess.Infrastructure.Domain.Users;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "users");

        builder.HasKey(k => k.Id);
    
        builder.Property(k => k.Id)
            .HasConversion(
                userId => userId.Value,
                value => UserId.Create(value))
            .ValueGeneratedNever()
            .HasColumnName("UserId");

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

        builder.Property(x => x.IsActive)
            .HasColumnName("IsActive");

        builder.Property(x => x.FirstName)
            .HasColumnName("FirstName")
            .HasMaxLength(70);

        builder.Property(x => x.LastName)
            .HasColumnName("LastName")
            .HasMaxLength(70);

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasMaxLength(155);

        builder.HasMany(r => r.Roles)
            .WithMany()
            .UsingEntity<Role>();

        builder.Property(x => x.Address)
            .HasColumnName("Address")
            .HasMaxLength(100);

        builder.Property(x => x.CreatedDateTime)
            .HasColumnName("CreatedDateTime");

        builder.Property(x => x.UpdatedDateTime)
            .HasColumnName("UpdatedDateTime");
    }
}
