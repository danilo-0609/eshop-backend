using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAccess.Domain.Users;
using UserAccess.Domain.Enums;

namespace UserAccess.Infrastructure.Domain.PermissionConfiguration;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions", "users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasColumnName("Name");

        var permissions = Enum.GetValues<Permissions>()
            .Select(x => new Permission
            {
                Id = (int)x,
                Name = x.ToString()
            });


        builder.HasData(permissions);

    }
}
