using Shopping.Application.Common;
using ErrorOr;
using MediatR;
using Shopping.Domain.Orders;
using Shopping.Domain.Orders.Errors;

namespace Shopping.Application.Orders.Expire;

internal sealed class ExpireOrderCommandHandler : ICommandRequestHandler<ExpireOrderCommand, ErrorOr<Unit>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly AuthorizationService _authorizationService;

    public ExpireOrderCommandHandler(IOrderRepository orderRepository, AuthorizationService authorizationService)
    {
        _orderRepository = orderRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Unit>> Handle(ExpireOrderCommand request, CancellationToken cancellationToken)
    {
        Order? order = await _orderRepository.GetByIdAsync(OrderId.Create(request.OrderId));

        if (order is null)
        {
            return OrderErrorCodes.NotFound;
        }

        var authorizationValidator = _authorizationService.IsUserAuthorized(order.CustomerId);

        if (authorizationValidator.IsError && _authorizationService.IsAdmin() is false)
        {
            return OrderErrorCodes.UserNotAuthorizedToAccess;
        }

        order.Expire(DateTime.UtcNow);

        await _orderRepository.UpdateAsync(order);

        return Unit.Value;
    }
}
