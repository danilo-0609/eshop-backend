using BuildingBlocks.Domain;
using ErrorOr;
using Shopping.Domain.Orders.Errors;

namespace Shopping.Domain.Orders.Rules;

internal class OrderCannotBeConfirmedWhenOrderStatusIsConfirmedRule : IBusinessRule
{
    private readonly OrderStatus _orderStatus;

    public OrderCannotBeConfirmedWhenOrderStatusIsConfirmedRule(OrderStatus orderStatus)
    {
        _orderStatus = orderStatus;
    }

    public Error Error => OrderErrors.OrderStatusAlreadyConfirmed;

    public bool IsBroken() => _orderStatus == OrderStatus.Confirmed;

    public static string Message => "Order cannot be confirmed when order status is confirmed";
}
