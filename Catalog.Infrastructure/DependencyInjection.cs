using BuildingBlocks.Application.EventBus;
using Catalog.Application.Common;
using Catalog.Domain.Comments;
using Catalog.Domain.Products;
using Catalog.Domain.Ratings;
using Catalog.Domain.Sales;
using Catalog.Infrastructure.Configuration.DbConnectionConfiguration;
using Catalog.Infrastructure.Domain.Comments;
using Catalog.Infrastructure.Domain.Products;
using Catalog.Infrastructure.Domain.Ratings;
using Catalog.Infrastructure.Domain.Sales;
using Catalog.Infrastructure.EventsBus;
using Catalog.Infrastructure.Outbox.BackgroundJobs;
using Catalog.Infrastructure.Outbox.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using static Quartz.Logging.OperationName;

namespace Catalog.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        services.AddQuartzHostedService();

        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessageJob));

            configure.AddJob<ProcessOutboxMessageJob>(jobKey, job => job.StoreDurably())
                .AddTrigger(
                    trigger =>
                        trigger.ForJob(jobKey)
                            .WithSimpleSchedule(
                                schedule =>
                                    schedule.WithIntervalInSeconds(10)
                                    .RepeatForever()));
        });
        
        services.AddDbContext<CatalogDbContext>((sp, optionsBuilder) => 
        {
            var interceptor = sp.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Database"))
                .AddInterceptors(interceptor);
        });


        //services.AddScoped<IUnitOfWork>(sp =>
           // sp.GetRequiredService<ApplicationDbContext>());
        
        services.AddScoped<IApplicationDbContext>(sp =>
            sp.GetRequiredService<CatalogDbContext>());
        

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IRatingRepository, RatingRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();

        services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();

        services.AddTransient<IEventBus, EventBus>();

        return services;
    }    
}
