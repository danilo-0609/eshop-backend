using BuildingBlocks.Application.Events;
using Shopping.Application.Common;
using Shopping.Domain.Payments.DomainEvents;
using Shopping.IntegrationEvents;

namespace Shopping.Application.Payments.Events;

internal sealed class PayMadeDomainEventHandler : IDomainEventHandler<PayMadeDomainEvent>
{
    private readonly IShoppingEventBus _eventBus;

    public PayMadeDomainEventHandler(IShoppingEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(PayMadeDomainEvent notification, CancellationToken cancellationToken)
    {
        await _eventBus.PublishAsync(new PayMadeIntegrationEvent(
            notification.DomainEventId,
            notification.PaymentId.Value,
            notification.OrderId.Value,
            notification.PayerId,
            notification.MoneyAmount,
            notification.ProductId,
            notification.OcurredOn));
    }
}
