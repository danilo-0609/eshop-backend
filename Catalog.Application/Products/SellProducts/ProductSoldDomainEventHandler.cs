using BuildingBlocks.Application.EventBus;
using BuildingBlocks.Application.Events;
using Catalog.Domain.Products.Events;
using Catalog.Domain.Sales;
using Catalog.IntegrationEvents;

namespace Catalog.Application.Products.SellProducts;
internal sealed class ProductSoldDomainEventHandler : IDomainEventHandler<ProductSoldDomainEvent>
{
    private readonly IEventBus _eventBus;
    private readonly ISaleRepository _saleRepository;

    public ProductSoldDomainEventHandler(IEventBus eventBus, ISaleRepository saleRepository)
    {
        _eventBus = eventBus;
        _saleRepository = saleRepository;
    }

    public async Task Handle(ProductSoldDomainEvent notification, CancellationToken cancellationToken)
    {
        await _eventBus.PublishAsync(new ProductSoldIntegrationEvent(
                notification.DomainEventId,
                notification.ProductId.Value,
                notification.Amount,
                notification.UnitPrice,
                notification.OrderId,
                notification.OcurredOn));

        Sale sale = Sale.Generate(
            notification.ProductId,
            notification.Amount,
            notification.UnitPrice,
            notification.UserId,
            DateTime.UtcNow);

        await _saleRepository.AddAsync(sale);
    }
}