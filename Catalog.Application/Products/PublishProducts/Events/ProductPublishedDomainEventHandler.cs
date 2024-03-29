using BuildingBlocks.Application.Events;
using Catalog.Application.Common;
using Catalog.Domain.Products.Events;
using Catalog.IntegrationEvents;

namespace Catalog.Application.Products.PublishProducts.Events;

internal sealed class ProductPublishedDomainEventHandler : IDomainEventHandler<ProductPublishedDomainEvent>
{
    private readonly IEventBus _eventBus;

    public ProductPublishedDomainEventHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }


    public async Task Handle(ProductPublishedDomainEvent notification, CancellationToken cancellationToken)
    {
        await _eventBus.PublishAsync(new ProductPublishedIntegrationEvent(
            notification.DomainEventId,
            notification.ProductId.Value,
            notification.SellerId,
            notification.Name,
            notification.Description,
            notification.Price,
            notification.InStock,
            notification.OcurredOn));
    }
}
