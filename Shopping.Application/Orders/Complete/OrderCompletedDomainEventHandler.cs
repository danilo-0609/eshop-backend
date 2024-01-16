using BuildingBlocks.Application.Events;
using MediatR;
using Shopping.Application.Buying.Generate;
using Shopping.Domain.Items;
using Shopping.Domain.Orders;
using Shopping.Domain.Orders.Events;

namespace Shopping.Application.Orders.Complete;

internal sealed class OrderCompletedDomainEventHandler : IDomainEventHandler<OrderCompletedDomainEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IItemRepository _itemRepository;
    private readonly ISender _sender;

    public OrderCompletedDomainEventHandler(IOrderRepository orderRepository, IItemRepository itemRepository, ISender sender)
    {
        _orderRepository = orderRepository;
        _itemRepository = itemRepository;
        _sender = sender;
    }

    public async Task Handle(OrderCompletedDomainEvent notification, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(notification.OrderId);

        var item = await _itemRepository.GetByIdAsync(order!.ItemId);

        await _sender.Send(new GenerateBuyCommand(
            notification.CustomerId,
            item!.Id,
            order.AmountOfItems,
            item!.Price));
    }
}
