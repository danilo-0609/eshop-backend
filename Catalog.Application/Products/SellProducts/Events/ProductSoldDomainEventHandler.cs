using BuildingBlocks.Application.EventBus;
using BuildingBlocks.Application.Events;
using Catalog.Application.Sales;
using Catalog.Domain.Products.Events;
using Catalog.IntegrationEvents;
using MediatR;

namespace Catalog.Application.Products.SellProducts.Events;
internal sealed class ProductSoldDomainEventHandler : IDomainEventHandler<ProductSoldDomainEvent>
{
    private readonly IEventBus _eventBus;
    private readonly ISender _sender;

    public ProductSoldDomainEventHandler(IEventBus eventBus, ISender sender)
    {
        _eventBus = eventBus;
        _sender = sender;
    }

    public async Task Handle(ProductSoldDomainEvent notification, CancellationToken cancellationToken)
    {
        await _eventBus.PublishAsync(new ProductSoldIntegrationEvent(
                notification.DomainEventId,
                notification.ProductId.Value,
                notification.Amount,
                notification.UnitPrice,
                notification.OcurredOn));

        await _sender.Send(new GenerateSaleCommand(
                notification.ProductId.Value,
                notification.Amount,
                notification.UnitPrice,
                notification.UserId));
    }
}