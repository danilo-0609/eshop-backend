using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UserAccess.Domain;
using UserAccess.Domain.UserRegistrations;
using UserAccess.Domain.Users;

namespace UserAccess.Infrastructure;

public partial class UserAccessDbContext : DbContext, IApplicationDbContext
{
    private readonly IConfiguration _configuration;

    public UserAccessDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<User> Users { get; set; }

    public DbSet<UserRegistration> UserRegistrations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserAccessDbContext).Assembly);

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK__Permissi__EFA6FB2F0FEB70D2");

            entity.ToTable("Permissions", "users");

            entity.Property(e => e.PermissionId).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1AAB8C5D2C");

            entity.ToTable("Roles", "users");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.RoleCode)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolesPermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RolesPermissions_Permissions"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RolesPermissions_Roles"),
                    j =>
                    {
                        j.HasKey("RoleId", "PermissionId");
                        j.ToTable("RolesPermissions", "users");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Database"));
    }

    public UserAccessDbContext() { }
}
