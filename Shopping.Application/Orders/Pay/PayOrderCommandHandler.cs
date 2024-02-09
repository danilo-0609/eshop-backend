using Shopping.Application.Common;
using ErrorOr;
using Shopping.Domain.Items;
using Shopping.Domain.Orders;
using Shopping.Domain.Orders.Errors;

namespace Shopping.Application.Orders.Pay;

internal sealed class PayOrderCommandHandler : ICommandRequestHandler<PayOrderCommand, ErrorOr<Guid>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IAuthorizationService _authorizationService;

    public PayOrderCommandHandler(IOrderRepository orderRepository, IItemRepository itemRepository, IAuthorizationService authorizationService)
    {
        _orderRepository = orderRepository;
        _itemRepository = itemRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Guid>> Handle(PayOrderCommand request, CancellationToken cancellationToken)
    {
        Order? order = await _orderRepository.GetByIdAsync(OrderId.Create(request.OrderId));

        if (order is null)
        {
            return OrderErrorCodes.NotFound;
        }

        var authorizationService = _authorizationService.IsUserAuthorized(order.CustomerId);

        if (authorizationService.IsError)
        {
            return OrderErrorCodes.UserNotAuthorizedToAccess;
        }

        var item = await _itemRepository.GetByIdAsync(order.ItemId);

        if (item is null)
        {
            return ItemErrorCodes.NotFound;
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
