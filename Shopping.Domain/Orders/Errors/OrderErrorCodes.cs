using ErrorOr;
using Shopping.Domain.Orders.Rules;

namespace Shopping.Domain.Orders.Errors;

public static class OrderErrorCodes
{
    public static Error OrderStatusIsNotPayed =>
        Error.Validation("Order.StatusIsNotPayed", OrderCannotBeCompletedWhenOrderStatusIsNotPayedRule.Message);

    public static Error OrderStatusAlreadyConfirmed =>
        Error.Validation("Order.StatusAlreadyConfirmed", OrderCannotBeConfirmedWhenOrderStatusIsConfirmedRule.Message);

    public static Error OrderStatusAlreadyExpired =>
        Error.Validation("Order.StatusAlreadyExpired", OrderCannotBeExpiredWhenOrderStatusIsExpiredRule.Message);

    public static Error ItemIsOutOfStock =>
        Error.Validation("Order.ItemIsOutOfStock", OrderCannotBePlacedWhenItemIsOutOfStockRule.Message);

    public static Error AmountRequestedGreaterThanActualStock =>
        Error.Validation("Order.AmountRequestedGreaterThanActualStock", OrderItemAmountCannotBeGreaterThanItemActualAmountRule.Message);

    public static Error OrderStatusIsCompleted =>
        Error.Validation("Order.StatusIsCompleted", OrderCannotBeExpiredAfterGetCompletedRule.Message);

    public static Error CannotPayedWhenStatusIsExpired =>
        Error.Validation("Order.StatusIsExpired", OrderCannotBePayedWhenOrderStatusIsExpiredRule.Message);

    public static Error CannotBePayedWhenStatusIsNotConfirmed =>
        Error.Validation("Order.StatusIsNotConfirmed", OrderCannotBePayedWhenOrderStatusIsNotConfirmedRule.Message);

    public static Error CannotBeConfirmWhenOrderStatusIsNotPlaced => 
        Error.Validation("Order.StatusIsNotPlaced", OrderCannotBeConfirmedWhenOrderStatusIsNotPlacedRule.Message);

    public static Error NotFound =>
        Error.NotFound("Order.NotFound", "Order was not found");

    public static Error UserNotAuthorizedToAccess =>
        Error.Unauthorized("Order.Unauthorized", "The user is not authorized to access in this content");
}
