using Shopping.Application.Common;
using ErrorOr;
using Shopping.Domain.Items;
using Shopping.Domain.Orders;

namespace Shopping.Application.Orders.Pay;

public sealed class PayOrderCommandHandler : ICommandRequestHandler<PayOrderCommand, ErrorOr<Guid>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IItemRepository _itemRepository;

    public PayOrderCommandHandler(IOrderRepository orderRepository, IItemRepository itemRepository)
    {
        _orderRepository = orderRepository;
        _itemRepository = itemRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(PayOrderCommand request, CancellationToken cancellationToken)
    {
        Order? order = await _orderRepository.GetByIdAsync(OrderId.Create(request.OrderId));

        if (order is null)
        {
            return Error.NotFound("Order.NotFound", "Order was not found");
        }

        var item = await _itemRepository.GetByIdAsync(order.ItemId);

        if (item is null)
        {
            return Error.NotFound("Item.NotFound", "Item in the order was not found");
        }

        var pay = order.Pay(DateTime.UtcNow,
            item.InStock,
            item.StockStatus);

        if (pay.IsError)
        {
            return pay.FirstError;
        }

        await _orderRepository.UpdateAsync(order);

        return order.Id.Value;
    }
}
