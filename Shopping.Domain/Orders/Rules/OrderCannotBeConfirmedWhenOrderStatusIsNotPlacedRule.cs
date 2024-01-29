using BuildingBlocks.Domain;
using ErrorOr;
using Shopping.Domain.Orders.Errors;

namespace Shopping.Domain.Orders.Rules;
internal sealed class OrderCannotBeConfirmedWhenOrderStatusIsNotPlacedRule : IBusinessRule
{
    private readonly OrderStatus _orderStatus;

    public OrderCannotBeConfirmedWhenOrderStatusIsNotPlacedRule(OrderStatus orderStatus)
    {
        _orderStatus = orderStatus;
    }

    public Error Error => OrderErrors.CannotBeConfirmWhenOrderStatusIsNotPlaced;

    public bool IsBroken() => _orderStatus != OrderStatus.Placed; 

    public static string Message => "Order cannot be confirmed when order status is not placed";
}
