using Catalog.Application.Common;
using Catalog.Domain.Comments;
using Catalog.Domain.Products;
using Catalog.Domain.Ratings;
using Catalog.Domain.Sales;
using Catalog.Infrastructure.Cache;
using Catalog.Infrastructure.Configuration.DbConnectionConfiguration;
using Catalog.Infrastructure.Domain.Comments;
using Catalog.Infrastructure.Domain.Products;
using Catalog.Infrastructure.Domain.Ratings;
using Catalog.Infrastructure.Domain.Sales;
using Catalog.Infrastructure.EventsBus;
using Catalog.Infrastructure.Outbox.BackgroundJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Catalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database")!;

        //Memory cache
        services.AddMemoryCache();

        //Background jobs
        services.AddQuartzHostedService();
        services.AddQuartz();
        services.ConfigureOptions<ProcessCatalogOutboxMessageJobSetup>();

        //Persistence. Database context
        services.AddDbContext<CatalogDbContext>((sp, optionsBuilder) => 
        {
            optionsBuilder.UseSqlServer(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(sp =>
            sp.GetRequiredService<CatalogDbContext>());

        //Unit of work
        services.AddScoped<ICatalogUnitOfWork, UnitOfWork>();

        //Repositories services
        services.AddScoped<IProductRepository, ProductRepository>();
        services.Decorate<IProductRepository, CachedProductRepository>();

        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IRatingRepository, RatingRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();

        //Dapper ORM connection
        services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();
        
        //Event bus service
        services.AddTransient<IEventBus, EventBus>();

        //Health checks
        services.AddHealthChecks()
            .AddDbContextCheck<CatalogDbContext>();

        return services;
    }    
}
