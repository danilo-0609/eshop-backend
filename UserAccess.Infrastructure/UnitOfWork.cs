using BuildingBlocks.Domain;
using Newtonsoft.Json;
using UserAccess.Application.Abstractions;
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
                IReadOnlyList<IDomainEvent> domainEvents = entity.GetDomainEvents();

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

        List<UserAccessOutboxMessage> outboxMessages = domainEvents
            .Select(domainEvent => new UserAccessOutboxMessage
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
                .UserAccessOutboxMessages
                .AddRangeAsync(outboxMessages);
    }
}
