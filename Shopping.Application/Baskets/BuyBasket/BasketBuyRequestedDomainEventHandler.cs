using BuildingBlocks.Application.Events;
using Shopping.Domain.Basket.Events;
using Shopping.Domain.Items;
using Shopping.Domain.Orders;

namespace Shopping.Application.Baskets.BuyBasket;

internal sealed class BasketBuyRequestedDomainEventHandler : IDomainEventHandler<BasketBuyRequestedDomainEvent>
{
    private readonly IItemRepository _itemRepository;

    public BasketBuyRequestedDomainEventHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task Handle(BasketBuyRequestedDomainEvent notification, CancellationToken cancellationToken)
    {
        foreach(var itemId in notification.ItemIds)
        {
            var item = await _itemRepository.GetByIdAsync(itemId);

            var order = Order.Place(
                item!.Id,
                notification.CustomerId,
                DateTime.UtcNow,
                1,
                item.InStock,
                notification.TotalAmount,
                item.StockStatus); 

            if (order.IsError)
            {
                break;
            }
        }
    }
}
