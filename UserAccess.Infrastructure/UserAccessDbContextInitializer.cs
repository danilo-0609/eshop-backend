using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace UserAccess.Infrastructure;

public static class IntializerExtensions
{
    public static async Task IntializeDatabaseAsync(this WebApplication web)
    {
        using var scope = web.Services.CreateScope();
    
        var intializer = scope.ServiceProvider.GetRequiredService<UserAccessDbContextInitializer>();

        await intializer.IntializeAsync();
    }
}

public sealed class UserAccessDbContextInitializer
{
    private readonly ILogger<UserAccessDbContextInitializer> _logger;
    private readonly UserAccessDbContext _context;

    public UserAccessDbContextInitializer(ILogger<UserAccessDbContextInitializer> logger, UserAccessDbContext userAccessDbContext)
    {
        _logger = logger;
        _context = userAccessDbContext;
    }

    public async Task IntializeAsync()
    {
        try
        {
            _logger.LogInformation("Starting user access context migration. ");

            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error ocurred while intializasing the database");
            throw;
        }
    }
}
