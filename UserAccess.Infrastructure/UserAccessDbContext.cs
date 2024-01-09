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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserAccessDbContext).Assembly);

        modelBuilder.Entity<RolePermission>()
            .HasKey(x => new { x.RoleId, x.PermissionId });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Database"));
    }

    public UserAccessDbContext() { }
}
