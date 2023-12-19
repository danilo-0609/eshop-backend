using BuildingBlocks.Application.EventBus;
using BuildingBlocks.Application.Events;
using Catalog.Domain.Products.Events;
using Catalog.IntegrationEvents;

namespace Catalog.Application.Products.RemoveProduct.Events;
internal sealed class ProductRemovedDomainEventHandler : IDomainEventHandler<ProductRemovedDomainEvent>
{
    private readonly IEventBus _eventBus;

    public ProductRemovedDomainEventHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(ProductRemovedDomainEvent notification, CancellationToken cancellationToken)
    {
        await _eventBus.PublishAsync(new ProductRemovedIntegrationEvent(
            notification.DomainEventId,
            notification.ProductId.Value,
            notification.OcurredOn));
    }
}
