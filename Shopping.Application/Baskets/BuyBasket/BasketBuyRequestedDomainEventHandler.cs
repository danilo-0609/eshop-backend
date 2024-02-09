using BuildingBlocks.Application.Events;
using Shopping.Application.Common;
using Shopping.Domain.Basket;
using Shopping.Domain.Basket.Events;
using Shopping.Domain.Items;
using Shopping.Domain.Orders;

namespace Shopping.Application.Baskets.BuyBasket;

internal sealed class BasketBuyRequestedDomainEventHandler : IDomainEventHandler<BasketBuyRequestedDomainEvent>
{
    private readonly IItemRepository _itemRepository;
    private readonly IBasketRepository _basketRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BasketBuyRequestedDomainEventHandler(IItemRepository itemRepository, IBasketRepository basketRepository, IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _itemRepository = itemRepository;
        _basketRepository = basketRepository;
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(BasketBuyRequestedDomainEvent notification, CancellationToken cancellationToken)
    {
        await PlaceOrders(notification);
    }

    private async Task PlaceOrders(BasketBuyRequestedDomainEvent notification)
    {
        foreach (Guid itemId in notification.ItemIds.Keys)
        {
            var item = await _itemRepository.GetByIdAsync(ItemId.Create(itemId));

            var order = Order.Place(
                item!.Id,
                notification.CustomerId,
                item.SellerId,
                DateTime.UtcNow,
                notification.ItemIds[itemId],
                item.InStock,
                item.Price,
                item.StockStatus);

            if (order.IsError)
            {
                break;

                throw new Exception("Buy operation failed");
            }

            await _orderRepository.AddAsync(order.Value);
        }

        await _unitOfWork.SaveChangesAsync();
    }
}
