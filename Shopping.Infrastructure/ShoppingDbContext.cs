using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shopping.Domain.Basket;
using Shopping.Domain.Buying;
using Shopping.Domain.Items;
using Shopping.Domain.Orders;
using Shopping.Domain.Payments;
using Shopping.Domain.Wishes;
using Shopping.Infrastructure.Outbox;

namespace Shopping.Infrastructure;

public sealed class ShoppingDbContext : DbContext, IApplicationDbContext
{
    private readonly IConfiguration _configuration;

    public ShoppingDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Basket> Baskets { get; set; }

    public DbSet<Buy> Buys { get; set; }

    public DbSet<Item> Items { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<Payment> Payments { get; set; }

    public DbSet<Wish> Wishes { get; set; }

    internal DbSet<ShoppingOutboxMessage> ShoppingOutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShoppingDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Database"));
    }
}