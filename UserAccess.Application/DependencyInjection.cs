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
            typeof(UserAccessApplicationLoggingPipelineBehavior<,>));

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(UserAccessValidationBehavior<,>));

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(UserAccessUnitOfWorkBehavior<,>));

        services.AddValidatorsFromAssemblyContaining<AssemblyReference>();

        return services;
    }
}