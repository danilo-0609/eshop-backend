using BuildingBlocks.Domain;
using ErrorOr;
using Shopping.Domain.Orders.Errors;

namespace Shopping.Domain.Orders.Rules;

internal sealed class OrderCannotBeConfirmedWhenOrderStatusIsPayedRule : IBusinessRule
{
    private readonly OrderStatus _orderStatus;

    public OrderCannotBeConfirmedWhenOrderStatusIsPayedRule(OrderStatus orderStatus)
    {
        _orderStatus = orderStatus;
    }

    public Error Error => OrderErrors.CannotBeConfirmedWhenOrderStatusIsPayed;

    public bool IsBroken() => _orderStatus == OrderStatus.Payed;

    public static string Message => "Order cannot be confirmed when order status is payed";
}
