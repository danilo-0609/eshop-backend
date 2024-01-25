using Catalog.Domain.Products;
using Catalog.Domain.Products.Services;
using MassTransit;
using Shopping.IntegrationEvents;

namespace Catalog.Application.Products.SellProducts;

public sealed class OrderPayedIntegrationEventConsumer : IConsumer<OrderPayedIntegrationEvent>
{
    private readonly IProductRepository _productRepository;

    public OrderPayedIntegrationEventConsumer(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Consume(ConsumeContext<OrderPayedIntegrationEvent> context)
    {
        var sellProductService = new SellProductService(_productRepository);

        await sellProductService.TrySellProduct(
            ProductId.Create(context.Message.ProductId),
            context.Message.OrderId,
            context.Message.AmountOfProducts);
    }
}
