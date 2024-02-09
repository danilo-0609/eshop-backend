using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;
using Shopping.Application.Common;
using Shopping.Domain.Orders;

namespace Shopping.Infrastructure.Jobs;

[DisallowConcurrentExecution]
internal sealed class ExpireOrdersJob : IJob
{
    private readonly ShoppingDbContext _dbContext;
    private readonly ILogger<ExpireOrdersJob> _logger;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ExpireOrdersJob(ShoppingDbContext dbContext, ILogger<ExpireOrdersJob> logger, IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _logger = logger;
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Starting {Name}, {OcurredOn}",
            nameof(ExpireOrdersJob),
            DateTime.UtcNow);

        var expirationLimit = DateTime.UtcNow.AddDays(-1);
        var ordersToExpire = await _dbContext
            .Orders
            .Where(d => d.ConfirmedOn != null && d.PayedOn == null && d.ConfirmedOn >= expirationLimit)
            .Take(20)
            .ToListAsync();

        foreach (var order in ordersToExpire)
        {
            order.Expire(DateTime.UtcNow);

            await _orderRepository.UpdateAsync(order);
        }

        await _unitOfWork.SaveChangesAsync();
    }
}

