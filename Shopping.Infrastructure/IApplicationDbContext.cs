using Microsoft.EntityFrameworkCore;
using Shopping.Domain.Basket;
using Shopping.Domain.Buying;
using Shopping.Domain.Items;
using Shopping.Domain.Orders;
using Shopping.Domain.Payments;
using Shopping.Domain.Wishes;

namespace Shopping.Infrastructure;

internal interface IApplicationDbContext
{
    public DbSet<Basket> Baskets { get; set; }

    public DbSet<Buy> Buys { get; set; }

    public DbSet<Item> Items { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<Payment> Payments { get; set; }

    public DbSet<Wish> Wishes { get; set; }
}