using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;
using Shopping.Domain.Orders;

namespace Shopping.Application.Orders.Complete;

internal sealed class CompleteOrderCommandHandler : ICommandRequestHandler<CompleteOrderCommand, ErrorOr<Unit>>
{
    private readonly IOrderRepository _orderRepository;

    public CompleteOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(CompleteOrderCommand request, CancellationToken cancellationToken)
    {
        Order? order = await _orderRepository.GetByIdAsync(OrderId.Create(request.OrderId));

        var complete = order!.Complete(DateTime.UtcNow);
        
        if (complete.IsError)
        {
            return complete.FirstError;
        }

        return Unit.Value;
    }
}
