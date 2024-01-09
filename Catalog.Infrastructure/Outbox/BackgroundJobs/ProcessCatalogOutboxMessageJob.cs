using BuildingBlocks.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace Catalog.Infrastructure.Outbox.BackgroundJobs;

[DisallowConcurrentExecution]
internal sealed class ProcessCatalogOutboxMessageJob : IJob
{
    private readonly CatalogDbContext _dbContext;
    private readonly IPublisher _publisher;
    private readonly ILogger<ProcessCatalogOutboxMessageJob> _logger;
    private readonly CancellationToken _cancellationToken;

    public ProcessCatalogOutboxMessageJob(
        CatalogDbContext dbContext,
        IPublisher publisher,
        ILogger<ProcessCatalogOutboxMessageJob> logger)
    {
        _dbContext = dbContext;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Starting execution of Job {Name}",
            nameof(ProcessCatalogOutboxMessageJob));

        List<CatalogOutboxMessage> messages = await _dbContext
            .CatalogOutboxMessages
            .Where(m => m.ProcessedOnUtc == null)
            .Take(20)
            .ToListAsync();

        foreach (CatalogOutboxMessage message in messages)
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
