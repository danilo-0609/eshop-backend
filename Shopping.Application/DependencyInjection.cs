﻿using FluentValidation;
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

        //Logging services
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ShoppingApplicationLoggingPipelineBehavior<,>));

        //Validator services
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        //Unit of work service
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(UnitOfWorkBehavior<,>));

        services.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);

        //Authorization service
        services.AddScoped(
            typeof(IAuthorizationService),
            typeof(AuthorizationService));

        return services;
    }
}
