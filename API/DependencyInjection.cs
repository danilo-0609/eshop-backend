using API.Common.Errors;
using API.Configuration;
using API.Middleware;
using API.OptionsSetup;
using BuildingBlocks.Application;
using BuildingBlocks.Infrastructure;
using MassTransit;
using Catalog.Application.IntegrationEventsConsumers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using UserAccess.Application.IntegrationEventCostumers;

namespace API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        //API Services 
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();


        //Authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();

        //Message broker

        services.Configure<MessageBrokerSettings>(
            configuration.GetSection("MessageBroker"));

        services.AddSingleton(sp =>
            sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();

            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();

                configurator.Host(new Uri(settings.Host), h =>
                {
                    h.Username(settings.Username);
                    h.Password(settings.Password);
                });
            });

            busConfigurator.AddConsumer<OrderConfirmedIntegrationEventConsumer>();
            busConfigurator.AddConsumer<ProductPublishedIntegrationEventConsumer>();
        });

        //Seed of work
        services.AddSingleton<Microsoft.AspNetCore.Mvc.ApiBehaviorOptions>();
        services.AddSingleton<Microsoft.AspNetCore.Http.HttpContextAccessor>();

        //Common handling services
        services.AddTransient<GlobalExceptionHandlingMiddleware>();
        services.AddTransient<EshopProblemDetailsFactory>();
        services.AddScoped<IExecutionContextAccessor, ExecutionContextAccessor>();

        return services;
    }
}
