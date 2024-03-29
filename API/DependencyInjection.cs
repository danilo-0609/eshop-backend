﻿using API.Common.Errors;
using API.Configuration;
using API.Middleware;
using API.OptionsSetup;
using BuildingBlocks.Application;
using BuildingBlocks.Infrastructure;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using UserAccess.Application.IntegrationEventCostumers;
using Catalog.Application.Products.SellProducts;
using Shopping.Application.Items.OutOfStock;
using Shopping.Application.Items.Remove;
using Shopping.Application.Items.Update;
using Shopping.Application.Orders.Complete;
using Shopping.Application.Orders.Expire;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Shopping.Application.Items.Create;
using Azure.Storage.Blobs;
using Azure.Identity;
using Microsoft.Extensions.DependencyInjection;
using UserAccess.Application.Common;

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
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.IncludeErrorDetails = true;
            opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "EshopApp",
                ValidAudience = "EshopService",
                IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("SuperSecretEshopKey"))
            };
        });

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

            busConfigurator.AddConsumer<OrderPayedIntegrationEventConsumer>();
            busConfigurator.AddConsumer<UserAccess.Application.IntegrationEventCostumers.ProductPublishedIntegrationEventConsumer>();
            busConfigurator.AddConsumer<Shopping.Application.Items.Create.ProductPublishedIntegrationEventConsumer>();
            busConfigurator.AddConsumer<ProductOutOfStockIntegrationEventConsumer>();
            busConfigurator.AddConsumer<ProductRemovedIntegrationEventConsumer>();
            busConfigurator.AddConsumer<ProductUpdatedIntegrationConsumer>();
            busConfigurator.AddConsumer<ProductSoldIntegrationEventConsumer>();
            busConfigurator.AddConsumer<ProductSellFailedIntegrationEventConsumer>();

            busConfigurator.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
            
        });

        //Azure Blob Storage
        services.AddSingleton(x => new BlobServiceClient(configuration.GetConnectionString("AzureBlobConnectionString")));

        //Health Checks
        services.AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("Database")!);

        //HttpContext
        services.AddSingleton<Microsoft.AspNetCore.Mvc.ApiBehaviorOptions>();
        services.AddSingleton<HttpContextAccessor>();
        services.AddHttpContextAccessor();

        //Common handling services
        services.AddTransient<GlobalExceptionHandlingMiddleware>();
        services.AddTransient<EshopProblemDetailsFactory>();
        services.AddScoped<IExecutionContextAccessor, ExecutionContextAccessor>();

        return services;
    }
}
