using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Shopping.Infrastructure;

public static class InitializerExtensions
{
    public static async Task IntializeDatabaseAsync(this WebApplication web)
    {
        using var scope = web.Services.CreateScope();

        var intializer = scope.ServiceProvider.GetRequiredService<ShoppingDbContextInitializer>();

        await intializer.InitializeAsync();
    }
}

public class ShoppingDbContextInitializer
{
    private readonly ILogger<ShoppingDbContextInitializer> _logger;
    private readonly ShoppingDbContext _dbContext;

    public ShoppingDbContextInitializer(ILogger<ShoppingDbContextInitializer> logger, ShoppingDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task InitializeAsync()
    {
        try
        {
            _logger.LogInformation("Starting shopping context migration");

            await _dbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error ocurred while initializing the shopping database");
            throw;
        }
    }
}
