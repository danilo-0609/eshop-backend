using BuildingBlocks.Application.Events;
using Shopping.Application.Common;
using Shopping.Domain.Orders.Events;
using Shopping.IntegrationEvents;

namespace Shopping.Application.Orders.Place;

internal sealed class OrderPlacedDomainEventHandler : IDomainEventHandler<OrderPlacedDomainEvent>
{
    private readonly IShoppingEventBus _shoppingEventBus;

    public OrderPlacedDomainEventHandler(IShoppingEventBus shoppingEventBus)
    {
        _shoppingEventBus = shoppingEventBus;
    }

    public async Task Handle(OrderPlacedDomainEvent notification, CancellationToken cancellationToken)
    {
        await _shoppingEventBus.PublishAsync(new OrderPlacedIntegrationEvent(
            notification.DomainEventId,
            notification.OrderId.Value,
            notification.CustomerId,
            notification.ItemId.Value,
            notification.OrderStatus.Value,
            notification.AmountRequested,
            notification.TotalMoneyAmount,
            notification.OcurredOn));
    }
}
