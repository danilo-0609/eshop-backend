using Shopping.Application.Common;
using ErrorOr;
using MediatR;
using Shopping.Domain.Orders;

namespace Shopping.Application.Orders.Expire;

internal sealed class ExpireOrderCommandHandler : ICommandRequestHandler<ExpireOrderCommand, ErrorOr<Unit>>
{
    private readonly IOrderRepository _orderRepository;

    public ExpireOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(ExpireOrderCommand request, CancellationToken cancellationToken)
    {
        Order? order = await _orderRepository.GetByIdAsync(OrderId.Create(request.OrderId));

        if (order is null)
        {
            return Error.NotFound("Order.NotFound", "Order was not found");
        }

        order.Expire(DateTime.UtcNow);

        await _orderRepository.UpdateAsync(order);

        return Unit.Value;
    }
}
