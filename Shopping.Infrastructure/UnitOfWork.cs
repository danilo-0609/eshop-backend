using BuildingBlocks.Domain;
using Newtonsoft.Json;
using Shopping.Application.Common;
using Shopping.Infrastructure.Outbox;

namespace Shopping.Infrastructure;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ShoppingDbContext _dbContext;

    public UnitOfWork(ShoppingDbContext context)
    {
        _dbContext = context;
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
            });

        _dbContext.ChangeTracker
         .Entries<IHasDomainEvents>()
         .Where(x => x.Entity.DomainEvents.Count() > 0)
         .Select(x =>
         {
             x.Entity.ClearDomainEvents();

             return 0;
         });


        List<ShoppingOutboxMessage> outboxMessages = domainEvents
            .Select(domainEvent => new ShoppingOutboxMessage
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
            .ShoppingOutboxMessages
            .AddRangeAsync(outboxMessages);
    }
}
