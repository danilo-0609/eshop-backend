using BuildingBlocks.Application.Events;
using Shopping.Domain.Basket.Events;

namespace Shopping.Application.Baskets.BuyBasket;

internal sealed class BasketBuyRequestedDomainEventHandler : IDomainEventHandler<BasketBuyRequestedDomainEvent>
{
    public Task Handle(BasketBuyRequestedDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
