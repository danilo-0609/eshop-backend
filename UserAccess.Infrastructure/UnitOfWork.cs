using BuildingBlocks.Domain;
using Newtonsoft.Json;
using UserAccess.Application.Common;
using UserAccess.Infrastructure.Outbox;

namespace UserAccess.Infrastructure;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly UserAccessDbContext _dbContext;

    public UnitOfWork(UserAccessDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ConvertDomainEventsToOutboxMessages();

        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async void ConvertDomainEventsToOutboxMessages()
    {
        List<IDomainEvent> domainEvents = _dbContext.ChangeTracker
            .Entries<IHasDomainEvents>()
            .Select(x => x.Entity)
            .SelectMany(entity =>
            {
                IReadOnlyList<IDomainEvent> domainEvents = entity.DomainEvents;

                entity.ClearDomainEvent();

                return domainEvents;
            }).ToList();

        List<OutboxMessage> outboxMessages = domainEvents
            .Select(domainEvent => new OutboxMessage
            {
                Id = domainEvent.DomainEventId,
                Type = domainEvent.GetType().Name,
                OcurredOnUtc = domainEvent.OcurredOn,
                Content = JsonConvert.SerializeObject(
                    domainEvent,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    })
            }).ToList();

            await _dbContext
                .OutboxMessages
                .AddRangeAsync(outboxMessages);
    }
}
