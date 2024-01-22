using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace Shopping.Infrastructure.Outbox.JobOutbox;

[DisallowConcurrentExecution]
internal sealed class ProcessShoppingOutboxMessageJob : IJob
{
    private readonly ShoppingDbContext _dbContext;
    private readonly IPublisher _publisher;
    private readonly ILogger<ProcessShoppingOutboxMessageJob> _logger;

    public ProcessShoppingOutboxMessageJob(
        ShoppingDbContext dbContext,
        IPublisher publisher,
        ILogger<ProcessShoppingOutboxMessageJob> logger)
    {
        _dbContext = dbContext;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Starting execution of job {Name}",
            nameof(ProcessShoppingOutboxMessageJob));

        List<ShoppingOutboxMessage> messages = await _dbContext
            .ShoppingOutboxMessages
            .Where(k => k.ProcessedOnUtc == null)
            .Take(20)
            .ToListAsync();

        foreach (ShoppingOutboxMessage message in messages)
        {
            var domainEvent = JsonConvert
                .DeserializeObject(message.Content);

            try
            {
                _logger.LogInformation("Starting publishing domain event {Name}, {OcurredOn}",
                    domainEvent.GetType().Name,
                    DateTime.UtcNow);

                await _publisher.Publish(domainEvent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Publishing error: {ex.Message} ");
            }

            message.ProcessedOnUtc = DateTime.UtcNow;
        }

        await _dbContext.SaveChangesAsync();
    }
}
