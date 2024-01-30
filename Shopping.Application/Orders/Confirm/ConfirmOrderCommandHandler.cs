using Shopping.Application.Common;
using ErrorOr;
using Shopping.Domain.Orders;
using Shopping.Domain.Orders.Errors;

namespace Shopping.Application.Orders.Confirm;

internal sealed class ConfirmOrderCommandHandler : ICommandRequestHandler<ConfirmOrderCommand, ErrorOr<Guid>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IAuthorizationService _authorizationService;

    public ConfirmOrderCommandHandler(IOrderRepository orderRepository, IAuthorizationService orderAuthorizationService)
    {
        _orderRepository = orderRepository;
        _authorizationService = orderAuthorizationService;
    }

    public async Task<ErrorOr<Guid>> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
    {
        Order? order = await _orderRepository.GetByIdAsync(OrderId.Create(request.OrderId));
    
        if (order is null)
        {
            return OrderErrorCodes.NotFound;
        }

        var authorizeService = _authorizationService.IsUserAuthorized(order.CustomerId);
        
        if (authorizeService.IsError)
        {
            return OrderErrorCodes.UserNotAuthorizedToAccess;
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
