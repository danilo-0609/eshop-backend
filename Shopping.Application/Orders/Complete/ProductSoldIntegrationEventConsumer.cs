using Catalog.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Shopping.Application.Orders.Complete;

public sealed class ProductSoldIntegrationEventConsumer : IConsumer<ProductSoldIntegrationEvent>
{
    private readonly ISender _sender;

    public ProductSoldIntegrationEventConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<ProductSoldIntegrationEvent> context)
    {
        await _sender.Send(new CompleteOrderCommand(context.Message.OrderId));
    }
}
