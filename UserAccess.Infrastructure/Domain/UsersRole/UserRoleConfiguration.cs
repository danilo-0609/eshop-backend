using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAccess.Domain.Users;

namespace UserAccess.Infrastructure.Domain.UsersRole;

internal sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UsersRoles", "users");

        builder.HasKey(x => new { x.UserId, x.RoleId });

        builder.Property(x => x.UserId).ValueGeneratedNever();

        builder.Property(x => x.RoleId).ValueGeneratedNever();
    }
}
