using BuildingBlocks.Application.EventBus;
using BuildingBlocks.Application.Events;
using Catalog.Domain.Products.Events;
using Catalog.IntegrationEvents;

namespace Catalog.Application.Products.SellProducts.Events;
internal sealed class ProductSellFailedDomainEventHandler : IDomainEventHandler<ProductSellFailedDomainEvent>
{
    private readonly IEventBus _eventBus;

    public ProductSellFailedDomainEventHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(ProductSellFailedDomainEvent notification, CancellationToken cancellationToken)
    {
        await _eventBus.PublishAsync(new ProductSellFailedIntegrationEvent(
            notification.DomainEventId,
            notification.ProductId.Value,
            notification.OrderId,
            notification.FailureCause,
            notification.OcurredOn));
    }
}