using BuildingBlocks.Domain;
using ErrorOr;
using Shopping.Domain.Orders.Errors;

namespace Shopping.Domain.Orders.Rules;

internal sealed class OrderCannotBePayedWhenOrderStatusIsExpiredRule : IBusinessRule
{
    private readonly OrderStatus _orderStatus;

    public OrderCannotBePayedWhenOrderStatusIsExpiredRule(OrderStatus orderStatus)
    {
        _orderStatus = orderStatus;
    }

    public Error Error => OrderErrorCodes.CannotPayedWhenStatusIsExpired;

    public bool IsBroken() => _orderStatus == OrderStatus.Expired;

    public static string Message => "Order cannot be payed when order status is expired";
}
