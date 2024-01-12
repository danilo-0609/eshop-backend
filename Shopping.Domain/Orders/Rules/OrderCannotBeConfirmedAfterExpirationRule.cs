using BuildingBlocks.Domain;
using ErrorOr;
using Shopping.Domain.Orders.Errors;

namespace Shopping.Domain.Orders.Rules;

internal sealed class OrderCannotBeConfirmedAfterExpirationRule : IBusinessRule
{
    private readonly OrderStatus _orderStatus;

    public OrderCannotBeConfirmedAfterExpirationRule(OrderStatus orderStatus)
    {
        _orderStatus = orderStatus;
    }

    public Error Error => OrderErrors.OrderStatusHasBeenExpired;

    public bool IsBroken() => _orderStatus == OrderStatus.Expired;

    public static string Message = "Order cannot be confirmed after expiration";
}
