using BuildingBlocks.Application.Events;
using Catalog.Application.Common;
using Catalog.Domain.Products.Events;
using Catalog.IntegrationEvents;

namespace Catalog.Application.Products.ModifyProduct.Events;

internal sealed class ProductUpdatedDomainEventHandler : IDomainEventHandler<ProductUpdatedDomainEvent>
{
    private readonly IEventBus _eventBus;

    public ProductUpdatedDomainEventHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(ProductUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await _eventBus.PublishAsync(new ProductUpdatedIntegrationEvent(
            notification.DomainEventId,
            notification.ProductId.Value,
            notification.Name,
            notification.SellerId,
            notification.Price,
            notification.InStock,
            notification.OcurredOn));
    }
}