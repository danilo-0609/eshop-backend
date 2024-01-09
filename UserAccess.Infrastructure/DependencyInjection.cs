using BuildingBlocks.Application.EventBus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using UserAccess.Application.Abstractions;
using UserAccess.Domain.UserRegistrations;
using UserAccess.Domain.Users;
using UserAccess.Infrastructure.Authentication;
using UserAccess.Infrastructure.Authorization;
using UserAccess.Infrastructure.Configuration.DbConnectionConfiguration;
using UserAccess.Infrastructure.Domain.UserRegistrations;
using UserAccess.Infrastructure.Domain.Users;
using UserAccess.Infrastructure.EventsBus;
using UserAccess.Infrastructure.Outbox.BackgroundJobs;

namespace UserAccess.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //Background jobs 
        services.AddQuartz();
        services.AddQuartzHostedService();
        services.ConfigureOptions<ProcessUserAccessOutboxMessageJobSetup>();

        //Persistence. Database context
        services.AddDbContext<UserAccessDbContext>((sp, optionsBuilder) =>
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Database"));
        });

        services.AddScoped<IApplicationDbContext>(sp =>
            sp.GetRequiredService<UserAccessDbContext>());

        //Unit of work.
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        //Repository and persistence services
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUserRegistrationRepository, UserRegistrationRepository>();

        services.AddScoped<IUsersCounter, UserRegistrationRepository>();

        services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();

        //Authentication
        services.AddTransient<IJwtProvider, JwtProvider>();

        //Authorization
        services.AddAuthorization();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider,  PermissionAuthorizationPolicyProvider>();

        //Event bus services
        services.AddTransient<IEventBus, EventBus>();

        return services;
    }
}
