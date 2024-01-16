using MassTransit;
using MediatR;
using Shopping.IntegrationEvents;

namespace Catalog.Application.Products.SellProducts;

internal sealed class OrderPayedIntegrationEventConsumer : IConsumer<OrderPayedIntegrationEvent>
{
    private readonly ISender _sender;

    public OrderPayedIntegrationEventConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<OrderPayedIntegrationEvent> context)
    {
        await _sender.Send(new SellProductCommand(
            context.Message.ProductId,
            context.Message.OrderId,
            context.Message.AmountOfProducts));        
    }
}
