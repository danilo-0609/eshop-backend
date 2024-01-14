using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shopping.Application.Common;

namespace Shopping.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //MediatR services
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<AssemblyReference>();
        });

        //Logging service
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ShoppingApplicationLoggingPipelineBehavior<,>));

        //Validator services
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ShoppingValidationBehavior<,>));

        //Unit of work service
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ShoppingUnitOfWorkBehavior<,>));

        services.AddValidatorsFromAssemblyContaining<AssemblyReference>();

        return services;
    }
}
