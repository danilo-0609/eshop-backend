using BuildingBlocks.Domain;
using ErrorOr;
using Shopping.Domain.Orders.Errors;

namespace Shopping.Domain.Orders.Rules;

internal sealed class OrderCannotBeExpiredAfterCompletationRule : IBusinessRule
{
    private readonly OrderStatus _orderStatus;

    public OrderCannotBeExpiredAfterCompletationRule(OrderStatus orderStatus)
    {
        _orderStatus = orderStatus;
    }

    public Error Error => OrderErrors.OrderStatusIsCompleted;

    public bool IsBroken() => _orderStatus == OrderStatus.Completed;

    public static string Message => "Order cannot be expired after completation";
}
