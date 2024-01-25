using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UserAccess.Domain;
using UserAccess.Domain.UserRegistrations;
using UserAccess.Domain.Users;
using UserAccess.Infrastructure.Outbox;

namespace UserAccess.Infrastructure;

public sealed class UserAccessDbContext : DbContext, IApplicationDbContext
{
    private readonly IConfiguration _configuration;

    public UserAccessDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<User> Users { get; set; }

    public DbSet<UserRegistration> UserRegistrations { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<Permission> Permissions { get; set; }

    public DbSet<RolePermission> RolePermissions { get; set; }

    public DbSet<UserAccessOutboxMessage> UserAccessOutboxMessages { get; set; }

    public DbSet<UserRole> UsersRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserAccessDbContext).Assembly);

        modelBuilder.Entity<RolePermission>()
            .HasKey(x => new { x.RoleId, x.PermissionId });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.UserId }).HasName("Pk_userRoles");

            entity.ToTable("UsersRoles", "users");

            entity.HasIndex(e => e.RoleId, "IX_UsersRoles_RoleId");

            entity.HasIndex(e => e.UserId, "IX_UsersRoles_UserId");
        });

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Database"));
    }

    public UserAccessDbContext() { }
}
