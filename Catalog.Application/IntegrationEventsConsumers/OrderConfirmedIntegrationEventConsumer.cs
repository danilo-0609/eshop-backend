using Catalog.Application.Products.SellProducts;
using Catalog.Domain.Products;
using MassTransit;
using MediatR;
using Shopping.IntegrationEvents;

namespace Catalog.Application.IntegrationEventsConsumers;

public sealed class OrderConfirmedIntegrationEventConsumer : IConsumer<OrderConfirmedIntegrationEvent>
{
    private readonly IProductRepository _productRepository;
    private readonly ISender _sender;

    public OrderConfirmedIntegrationEventConsumer(IProductRepository productRepository, ISender sender)
    {
        _productRepository = productRepository;
        _sender = sender;

    }

    public async Task Consume(ConsumeContext<OrderConfirmedIntegrationEvent> context)
    {
        Product? product = await _productRepository
            .GetByIdAsync(ProductId.Create(context.Message.ProductId));

        if (product!.InStock == 0)
        {
            product.OutOfStock();
        }

        await _sender.Send(new SellProductCommand(
                product.Id.Value, 
                context.Message.AmountOfProducts));
    }

}
