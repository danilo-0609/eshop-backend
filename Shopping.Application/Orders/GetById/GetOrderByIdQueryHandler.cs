using ErrorOr;
using Shopping.Application.Common;
using Shopping.Domain.Orders;

namespace Shopping.Application.Orders.GetById;

internal sealed class GetOrderByIdQueryHandler : ICommandRequestHandler<GetOrderByIdQuery, ErrorOr<OrderResponse>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<OrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        Order? order = await _orderRepository.GetByIdAsync(OrderId.Create(request.OrderId));
    
        if (order is null)
        {
            return Error.NotFound("Order.NotFound", "Order was not found");
        }

        return new OrderResponse(
            order.Id.Value,
            order.CustomerId,
            order.ItemId.Value,
            order.AmountOfItems,
            order.TotalMoneyAmount,
            order.OrderStatus.Value,
            order.PlacedOn);
    }
}
