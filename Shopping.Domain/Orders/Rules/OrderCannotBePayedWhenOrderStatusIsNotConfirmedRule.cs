using BuildingBlocks.Domain;
using ErrorOr;
using Shopping.Domain.Orders.Errors;

namespace Shopping.Domain.Orders.Rules;

internal sealed class OrderCannotBePayedWhenOrderStatusIsNotConfirmedRule : IBusinessRule
{
    private readonly OrderStatus _orderStatus;

    public OrderCannotBePayedWhenOrderStatusIsNotConfirmedRule(OrderStatus orderStatus)
    {
        _orderStatus = orderStatus;
    }

    public Error Error => OrderErrors.CannotBePayedWhenStatusIsNotConfirmed;

    public bool IsBroken() => _orderStatus != OrderStatus.Confirmed;

    public static string Message => "Order cannot be payed when order status is not confirmed";
}
