using BuildingBlocks.Application.EventBus;
using BuildingBlocks.Application.Events;
using Catalog.Domain.Products.Events;
using Catalog.IntegrationEvents;

namespace Catalog.Application.Products.OutOfStock;

public sealed class ProductOutOfStockDomainEventHandler : IDomainEventHandler<ProductOutOfStockDomainEvent>
{
    private readonly IEventBus _eventBus;

    public ProductOutOfStockDomainEventHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(ProductOutOfStockDomainEvent notification, CancellationToken cancellationToken)
    {
        await _eventBus.PublishAsync(new ProductOutOfStockIntegrationEvent(
            notification.DomainEventId,
            notification.ProductId.Value,
            notification.OcurredOn));
    }

}
