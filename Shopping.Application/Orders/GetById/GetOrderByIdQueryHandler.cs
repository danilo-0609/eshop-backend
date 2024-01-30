using ErrorOr;
using Shopping.Application.Common;
using Shopping.Domain.Orders;
using Shopping.Domain.Orders.Errors;

namespace Shopping.Application.Orders.GetById;

internal sealed class GetOrderByIdQueryHandler : ICommandRequestHandler<GetOrderByIdQuery, ErrorOr<OrderResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly AuthorizationService _authorizationService;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository, AuthorizationService authorizationService)
    {
        _orderRepository = orderRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<OrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        Order? order = await _orderRepository.GetByIdAsync(OrderId.Create(request.OrderId));
    
        if (order is null)
        {
            return OrderErrorCodes.NotFound;
        }

        var authorizationService = _authorizationService.IsUserAuthorized(order.CustomerId);

        if (authorizationService.IsError && _authorizationService.IsAdmin() is false)
        {
            return OrderErrorCodes.UserNotAuthorizedToAccess;
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
