using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UserAccess.Application.Common;

namespace UserAccess.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(LoggingPipelineBehavior<,>));

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(UnitOfWorkBehavior<,>));

        services.AddValidatorsFromAssemblyContaining<AssemblyReference>();

        return services;
    }
}