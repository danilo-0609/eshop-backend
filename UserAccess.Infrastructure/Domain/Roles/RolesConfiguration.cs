using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAccess.Domain;
using UserAccess.Domain.Users;

namespace UserAccess.Infrastructure.Domain.Roles;

internal sealed class RolesConfiguration : IEntityTypeConfiguration<Role>
{
    private readonly DbContextOptionsBuilder _dbContextOptionsBuilder;

    public RolesConfiguration(DbContextOptionsBuilder dbContextOptionsBuilder)
    {
        _dbContextOptionsBuilder = dbContextOptionsBuilder;
    }

    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles", "users");

        builder.HasKey(r => r.RoleId);

        builder.Property(r => r.RoleId).ValueGeneratedNever().HasColumnName("RoleId");

        builder.Property(r => r.RoleCode).HasColumnName("RoleCode");

        builder.HasMany(p => p.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();

        builder.HasData(new Role[]
        {
            new Role
            {
                RoleId = Role.Administrator.RoleId,
                RoleCode = Role.Administrator.RoleCode
            },
            new Role
            {
                RoleId = Role.Customer.RoleId,
                RoleCode = Role.Customer.RoleCode
            },
            new Role
            {
                RoleId = Role.Seller.RoleId,
                RoleCode = Role.Seller.RoleCode
            },
        });

        _dbContextOptionsBuilder.EnableSensitiveDataLogging();
    }
}
