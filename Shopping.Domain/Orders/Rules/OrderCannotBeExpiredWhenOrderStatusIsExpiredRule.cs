using BuildingBlocks.Domain;
using ErrorOr;
using Shopping.Domain.Orders.Errors;

namespace Shopping.Domain.Orders.Rules;

internal sealed class OrderCannotBeExpiredWhenOrderStatusIsExpiredRule : IBusinessRule
{
    private readonly OrderStatus _orderStatus;

    public OrderCannotBeExpiredWhenOrderStatusIsExpiredRule(OrderStatus orderStatus)
    {
        _orderStatus = orderStatus;
    }

    public Error Error => OrderErrors.OrderStatusAlreadyExpired;

    public bool IsBroken() => _orderStatus == OrderStatus.Expired;

    public static string Message => "Order cannot be expired when order status is expired";
}
