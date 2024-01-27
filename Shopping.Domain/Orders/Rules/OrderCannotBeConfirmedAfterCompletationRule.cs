using BuildingBlocks.Domain;
using ErrorOr;
using Shopping.Domain.Orders.Errors;

namespace Shopping.Domain.Orders.Rules;

internal sealed class OrderCannotBeConfirmedAfterCompletationRule : IBusinessRule
{
    private readonly OrderStatus _orderStatus;

    public OrderCannotBeConfirmedAfterCompletationRule(OrderStatus orderStatus)
    {
        _orderStatus = orderStatus;
    }

    public Error Error => OrderErrors.CannotBeConfirmedAfterCompletation;

    public bool IsBroken() => _orderStatus == OrderStatus.Completed;

    public static string Message => "Order cannot be confirmed after completation";
}
