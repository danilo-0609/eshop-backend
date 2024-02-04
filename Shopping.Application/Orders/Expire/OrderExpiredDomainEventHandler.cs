using BuildingBlocks.Application.Events;
using Shopping.Application.Common;
using Shopping.Domain.Orders;
using Shopping.Domain.Orders.Events;
using Shopping.IntegrationEvents;

namespace Shopping.Application.Orders.Expire;

internal sealed class OrderExpiredDomainEventHandler : IDomainEventHandler<OrderExpiredDomainEvent>
{
    private readonly IShoppingEventBus _shoppingEventBus;

    public OrderExpiredDomainEventHandler(IShoppingEventBus shoppingEventBus)
    {
        _shoppingEventBus = shoppingEventBus;
    }

    public async Task Handle(OrderExpiredDomainEvent notification, CancellationToken cancellationToken)
    {
        await _shoppingEventBus.PublishAsync(new OrderExpiredIntegrationEvent(
            notification.DomainEventId,
            notification.OrderId.Value,
            notification.OcurredOn));
    }
}
