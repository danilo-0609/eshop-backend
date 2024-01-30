using BuildingBlocks.Domain;
using ErrorOr;
using Shopping.Domain.Orders.Errors;

namespace Shopping.Domain.Orders.Rules;

internal sealed class OrderCannotBeExpiredAfterGetCompletedRule : IBusinessRule
{
    private readonly OrderStatus _orderStatus;

    public OrderCannotBeExpiredAfterGetCompletedRule(OrderStatus orderStatus)
    {
        _orderStatus = orderStatus;
    }

    public Error Error => OrderErrorCodes.OrderStatusIsCompleted;

    public bool IsBroken() => _orderStatus == OrderStatus.Completed;

    public static string Message => "Order cannot be expired after get completed";
}
