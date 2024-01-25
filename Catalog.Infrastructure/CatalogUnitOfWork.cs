using BuildingBlocks.Domain;
using Catalog.Application.Common;
using Catalog.Infrastructure.Outbox;
using Newtonsoft.Json;

namespace Catalog.Infrastructure;

internal sealed class CatalogUnitOfWork : ICatalogUnitOfWork
{
    private readonly CatalogDbContext _dbContext;

    public CatalogUnitOfWork(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await ConvertDomainEventsToOutboxMessages();

        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task ConvertDomainEventsToOutboxMessages()
    {
        var domainEvents = _dbContext.ChangeTracker
            .Entries<IHasDomainEvents>()
            .Select(x => x.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.DomainEvents;

                entity.ClearDomainEvents();

                return domainEvents;
            }).ToList();

        _dbContext.ChangeTracker
            .Entries<IHasDomainEvents>()
            .Where(x => x.Entity.DomainEvents.Count() > 0)
            .Select(x =>
            {
                x.Entity.ClearDomainEvents();

                return 0;
            });
            

        List<CatalogOutboxMessage> outboxMessages = domainEvents
            .Select(domainEvent => new CatalogOutboxMessage
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
            .CatalogOutboxMessages
            .AddRangeAsync(outboxMessages);
    }
}
