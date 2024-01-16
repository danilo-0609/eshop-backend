using BuildingBlocks.Application.Commands;
using ErrorOr;
using Shopping.Domain.Orders;

namespace Shopping.Application.Orders.Confirm;

internal sealed class ConfirmOrderCommandHandler : ICommandRequestHandler<ConfirmOrderCommand, ErrorOr<Guid>>
{
    private readonly IOrderRepository _orderRepository;

    public ConfirmOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
    {
        Order? order = await _orderRepository.GetByIdAsync(OrderId.Create(request.OrderId));
    
        if (order is null)
        {
            return Error.NotFound("Order.NotFound", "Order was not found");
        }

        var confirm = order.Confirm(DateTime.UtcNow);

        if (confirm.IsError)
        {
            return confirm.FirstError;
        }

        await _orderRepository.UpdateAsync(order);

        return order.Id.Value;
    }
}
