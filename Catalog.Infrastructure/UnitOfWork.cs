using BuildingBlocks.Domain;
using Catalog.Application.Common;
using Catalog.Infrastructure.Outbox;
using Newtonsoft.Json;

namespace Catalog.Infrastructure;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly CatalogDbContext _dbContext;

    public UnitOfWork(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ConvertDomainEventsToOutboxMessages();

        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private void ConvertDomainEventsToOutboxMessages()
    {
        var domainEvents = _dbContext.ChangeTracker
            .Entries<IHasDomainEvents>()
            .Select(x => x.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.DomainEvents;

                entity.ClearDomainEvent();

                return domainEvents;
            })
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

        _dbContext.OutboxMessages.AddRange(domainEvents);
    }
}
