using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UserAccess.Domain.UserRegistrations;
using UserAccess.Domain.Users;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasKey(x => x.Id);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserAccessDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Database"));
    }

    public UserAccessDbContext() { }
}
