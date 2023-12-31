using BuildingBlocks.Application.EventBus;
using MediatR;
using UserAccess.Domain.UserRegistrations.Events;
using UserAccess.IntegrationEvents;

namespace UserAccess.Application.UserRegistration.RegisterNewUser;

internal sealed class NewUserRegisteredDomainEventHandler : INotificationHandler<NewUserRegisteredDomainEvent>
{
    private readonly IEventBus _eventBus;

    public NewUserRegisteredDomainEventHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(NewUserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        await _eventBus.PublishAsync(new 
            NewUserRegisteredIntegrationEvent(
                notification.DomainEventId,
                notification.Id.Value,
                notification.Login,
                notification.Email,
                notification.FirstName,
                notification.LastName,
                notification.Name,
                notification.OcurredOn));
    }
}
