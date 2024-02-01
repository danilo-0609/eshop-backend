using BuildingBlocks.Application;
using Shopping.Application.Common;
using ErrorOr;
using Shopping.Domain.Items;
using Shopping.Domain.Orders;

namespace Shopping.Application.Orders.Place;

internal sealed class PlaceOrderCommandHandler : ICommandRequestHandler<PlaceOrderCommand, ErrorOr<Guid>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public PlaceOrderCommandHandler(IOrderRepository orderRepository, IItemRepository itemRepository, IExecutionContextAccessor executionContextAccessor)
    {
        _orderRepository = orderRepository;
        _itemRepository = itemRepository;
        _executionContextAccessor = executionContextAccessor;
    }

    public async Task<ErrorOr<Guid>> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        Item? item = await _itemRepository.GetByIdAsync(ItemId.Create(request.ItemId));

        if (item is null)
        {
            return Error.NotFound("Item.NotFound", "Item was not found");
        }

        ErrorOr<Order> order = Order.Place(
            item.Id,
            _executionContextAccessor.UserId,
            item.SellerId,
            DateTime.UtcNow,
            request.AmountRequested,
            item.InStock,
            item.Price * request.AmountRequested,
            item.StockStatus);

        if (order.IsError)
        {
            return order.FirstError;
        }

        await _orderRepository.AddAsync(order.Value);

        return order.Value.Id.Value;
    }
}
