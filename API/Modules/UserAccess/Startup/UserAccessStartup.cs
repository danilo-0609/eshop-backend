using UserAccess.Application;
using UserAccess.Infrastructure;

namespace API.Modules.UserAccess.Startup;

public static class UserAccessStartup
{
    public static IServiceCollection AddUserAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication();
        services.AddInfrastructure(configuration);

        return services;
    }
}
