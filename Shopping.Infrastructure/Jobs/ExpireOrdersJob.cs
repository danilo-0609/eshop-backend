using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Shopping.Infrastructure.Jobs;

[DisallowConcurrentExecution]
internal sealed class ExpireOrdersJob : IJob
{
    private readonly ShoppingDbContext _dbContext;
    private readonly ILogger<ExpireOrdersJob> _logger;

    public ExpireOrdersJob(ShoppingDbContext dbContext, ILogger<ExpireOrdersJob> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Starting {Name}, {OcurredOn}",
            nameof(ExpireOrdersJob),
            DateTime.UtcNow);

        var ordersToExpire = await _dbContext
            .Orders
            .Where(d => d.ConfirmedOn != null && d.PayedOn == null && d.ConfirmedOn >= DateTime.UtcNow.AddDays(-1))
            .Take(20)
            .ToListAsync();

        foreach (var order in ordersToExpire)
        {
            order.Expire(DateTime.UtcNow);
        }
    }
}

