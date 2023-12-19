using Catalog.Application;
using Catalog.Infrastructure;

namespace API.Modules.Catalog.Startup;

public static class CatalogStartup
{
    public static IServiceCollection AddCatalog(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication();
        services.AddInfrastructure(configuration);

        return services;
    }
}
