using BuildingBlocks.Application.EventBus;
using BuildingBlocks.Application.Events;
using UserAccess.Domain.Users.Events;
using UserAccess.IntegrationEvents;

namespace UserAccess.Application.Users.Create;

internal sealed class UserCreatedDomainEventHandler : IDomainEventHandler<UserCreatedDomainEvent>
{
    private readonly IEventBus _eventBus;

    public UserCreatedDomainEventHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await _eventBus.PublishAsync(new UserCreatedIntegrationEvent(
            notification.DomainEventId,
            notification.UserId.Value,
            notification.OcurredOn));
    }
}
