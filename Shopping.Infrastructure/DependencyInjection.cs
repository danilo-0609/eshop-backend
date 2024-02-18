using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Shopping.Application.Common;
using Shopping.Domain.Basket;
using Shopping.Domain.Buying;
using Shopping.Domain.Items;
using Shopping.Domain.Orders;
using Shopping.Domain.Payments;
using Shopping.Domain.Wishes;
using Shopping.Infrastructure.Domain.Baskets;
using Shopping.Infrastructure.Domain.Buying;
using Shopping.Infrastructure.Domain.Items;
using Shopping.Infrastructure.Domain.Orders;
using Shopping.Infrastructure.Domain.Payments;
using Shopping.Infrastructure.Domain.Wishes;
using Shopping.Infrastructure.EventsBus;
using Shopping.Infrastructure.Jobs;
using Shopping.Infrastructure.Outbox.JobOutbox;

namespace Shopping.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database")!;

        //Background jobs
        services.AddQuartz();
        services.AddQuartzHostedService();
        services.ConfigureOptions<ProcessShoppingOutboxMessageJobSetup>();
        services.ConfigureOptions<ExpireOrdersJobSetup>();

        //Persistence. Database context
        services.AddDbContext<ShoppingDbContext>((optionsBuilder) =>
        {
            optionsBuilder.UseSqlServer(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(sp =>
            sp.GetRequiredService<ShoppingDbContext>());

        //Unit of work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        //Repositories
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<IBuyRepository, BuyRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IWishRepository, WishRepository>();

        //Event bus
        services.AddTransient<IShoppingEventBus, EventBus>();

        //HealthChecks

        services.AddHealthChecks()
            .AddDbContextCheck<ShoppingDbContext>();

        return services;
    }
}

