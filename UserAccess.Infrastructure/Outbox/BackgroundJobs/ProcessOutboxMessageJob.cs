using BuildingBlocks.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace UserAccess.Infrastructure.Outbox.BackgroundJobs;

[DisallowConcurrentExecution]
internal sealed class ProcessOutboxMessageJob : IJob
{
    private readonly UserAccessDbContext _dbContext;
    private readonly IPublisher _publisher;
    private readonly ILogger<ProcessOutboxMessageJob> _logger;
    private readonly CancellationToken _cancellationToken;

    public ProcessOutboxMessageJob(
        UserAccessDbContext dbContext,
        IPublisher publisher,
        ILogger<ProcessOutboxMessageJob> logger)
    {
        _dbContext = dbContext;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        List<OutboxMessage> messages = await _dbContext
            .OutboxMessages
            .Where(m => m.ProcessedOnUtc == null)
            .Take(20)
            .ToListAsync();

        foreach (OutboxMessage message in messages)
        {
            IDomainEvent? domainEvent = JsonConvert
                .DeserializeObject<IDomainEvent>(message.Content);

            try
            {
                await _publisher.Publish(domainEvent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Publishing error: {ex.Message} ");
            }

            message.ProcessedOnUtc = DateTime.UtcNow;
        }

        await _dbContext.SaveChangesAsync(_cancellationToken);
    }
}
