using BuildingBlocks.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace UserAccess.Infrastructure.Outbox.BackgroundJobs;

[DisallowConcurrentExecution]
internal sealed class ProcessUserAccessOutboxMessageJob : IJob
{
    private readonly UserAccessDbContext _dbContext;
    private readonly IPublisher _publisher;
    private readonly ILogger<ProcessUserAccessOutboxMessageJob> _logger;
    private readonly CancellationToken _cancellationToken;

    public ProcessUserAccessOutboxMessageJob(
        UserAccessDbContext dbContext,
        IPublisher publisher,
        ILogger<ProcessUserAccessOutboxMessageJob> logger)
    {
        _dbContext = dbContext;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Starting execution of Job {Name}",
            nameof(ProcessUserAccessOutboxMessageJob));

        List<UserAccessOutboxMessage> messages = await _dbContext
            .UserAccessOutboxMessages
            .Where(m => m.ProcessedOnUtc == null)
            .Take(20)
            .ToListAsync();

        foreach (UserAccessOutboxMessage message in messages)
        {
            IDomainEvent? domainEvent = JsonConvert
                .DeserializeObject<IDomainEvent>(
                    message.Content, 
                    new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });

            if (domainEvent is null)
            {
                _logger.LogError("Domain event is null when publishing");
            }

            try
            {
                _logger.LogInformation("Starting publishing domain event {Name}, {OcurredOn}",
                    domainEvent!.GetType().Name,
                    DateTime.UtcNow);

                await _publisher.Publish(domainEvent!, context.CancellationToken);
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
