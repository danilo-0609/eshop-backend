using Catalog.Application.Common;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Catalog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config => 
        {
            config.RegisterServicesFromAssemblyContaining<AssemblyReference>();
        });

        services.AddScoped(
            typeof(IPipelineBehavior<,>),   
            typeof(CatalogValidationBehavior<,>));

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(CatalogApplicationLoggingPipelineBehavior<,>));

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(CatalogUnitOfWorkBehavior<,>));

        services.AddValidatorsFromAssemblyContaining<AssemblyReference>();
        
        return services;
    }
}
