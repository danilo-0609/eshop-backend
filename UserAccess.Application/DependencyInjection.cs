using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UserAccess.Application.Abstractions;
using UserAccess.Application.Common;

namespace UserAccess.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<AssemblyReference>();

            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssemblyContaining<AssemblyReference>(includeInternalTypes: true);

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(UserAccessApplicationLoggingPipelineBehavior<,>));

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(UnitOfWorkBehavior<,>));

        services.AddScoped(
            typeof(IAuthorizationService),
            typeof(AuthorizationService));

        return services;
    }
}