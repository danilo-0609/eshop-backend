using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure;

public static class IntializerExtensions
{
    public static async Task IntializeDatabaseAsync(this WebApplication web)
    {
        using var scope = web.Services.CreateScope();

        var intializer = scope.ServiceProvider.GetRequiredService<CatalogDbContextIntializer>();

        await intializer.InItializeAsync();
    }
}

public sealed class CatalogDbContextIntializer
{
    private readonly ILogger<CatalogDbContextIntializer> _logger;
    private readonly CatalogDbContext _context;

    public CatalogDbContextIntializer(ILogger<CatalogDbContextIntializer> logger, CatalogDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InItializeAsync()
    {
        try
        {
            _logger.LogInformation("Starting catalog context migration.");

            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error ocurred while intializasing the catalog database");
            throw;
        }
    }
}
