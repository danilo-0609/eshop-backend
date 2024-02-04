using Shopping.Application.Common;
using ErrorOr;
using Shopping.Domain.Orders;
using Shopping.Domain.Orders.Errors;
using Shopping.Domain.Items;

namespace Shopping.Application.Orders.Confirm;

internal sealed class ConfirmOrderCommandHandler : ICommandRequestHandler<ConfirmOrderCommand, ErrorOr<Guid>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IAuthorizationService _authorizationService;
    private readonly IItemRepository _itemRepository;

    public ConfirmOrderCommandHandler(IOrderRepository orderRepository, IAuthorizationService orderAuthorizationService, IItemRepository itemRepository)
    {
        _orderRepository = orderRepository;
        _authorizationService = orderAuthorizationService;
        _itemRepository = itemRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
    {
        Order? order = await _orderRepository.GetByIdAsync(OrderId.Create(request.OrderId));
    
        if (order is null)
        {
            return OrderErrorCodes.NotFound;
        }

        Guid? sellerId = _itemRepository.GetSellerId(order.ItemId);

        var authorizeService = _authorizationService.IsUserAuthorized((Guid)sellerId!);
        
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
