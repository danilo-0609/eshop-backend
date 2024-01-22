using Shopping.Application;
using Shopping.Infrastructure;

namespace API.Modules.Shopping.Startup;

public static class ShoppingStartup
{
    public static IServiceCollection AddShopping(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication();
        services.AddInfrastructure(configuration);

        return services;
    }
}
