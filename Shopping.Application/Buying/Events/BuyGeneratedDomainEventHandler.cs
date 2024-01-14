using BuildingBlocks.Application.Events;
using Shopping.Application.Common;
using Shopping.Domain.Buying.Events;
using Shopping.IntegrationEvents;

namespace Shopping.Application.Buying.Events;

internal sealed class BuyGeneratedDomainEventHandler : IDomainEventHandler<BuyGeneratedDomainEvent>
{
    private readonly IShoppingEventBus _eventBus;

    public BuyGeneratedDomainEventHandler(IShoppingEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(BuyGeneratedDomainEvent notification, CancellationToken cancellationToken)
    {
        await _eventBus.PublishAsync(new BuyGeneratedIntegrationEvent(
            notification.DomainEventId,
            notification.BuyId.Value,
            notification.BuyerId,
            notification.ItemId.Value,
            notification.UnitPrice,
            notification.TotalPrice,
            notification.AmountOfProducts,
            DateTime.UtcNow));
    }
}
