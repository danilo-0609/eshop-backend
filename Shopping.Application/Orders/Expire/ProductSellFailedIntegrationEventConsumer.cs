using Catalog.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Shopping.Application.Orders.Expire;

public sealed class ProductSellFailedIntegrationEventConsumer : IConsumer<ProductSellFailedIntegrationEvent>
{
    private readonly ISender _sender;

    public ProductSellFailedIntegrationEventConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<ProductSellFailedIntegrationEvent> context)
    {
        await _sender.Send(new ExpireOrderCommand(context.Message.OrderId));
    }
}
