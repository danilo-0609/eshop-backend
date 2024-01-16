using BuildingBlocks.Application.Events;
using Shopping.Application.Common;
using Shopping.Domain.Orders.Events;
using Shopping.IntegrationEvents;

namespace Shopping.Application.Orders.Confirm;

internal sealed class OrderConfirmedDomainEventHandler : IDomainEventHandler<OrderConfirmedDomainEvent>
{
    private readonly IShoppingEventBus _eventBus;

    public OrderConfirmedDomainEventHandler(IShoppingEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(OrderConfirmedDomainEvent notification, CancellationToken cancellationToken)
    {
        await _eventBus.PublishAsync(new OrderConfirmedIntegrationEvent(
            notification.DomainEventId,
            notification.OrderId.Value,
            DateTime.UtcNow));
    }
}
