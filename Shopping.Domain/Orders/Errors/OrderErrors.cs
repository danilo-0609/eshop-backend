using ErrorOr;
using Shopping.Domain.Orders.Rules;

namespace Shopping.Domain.Orders.Errors;

public static class OrderErrors
{
    public static Error OrderStatusIsNotPayed =>
        Error.Validation("Order.StatusIsNotPayed", OrderCannotBeCompletedWhenOrderStatusIsNotPayedRule.Message);

    public static Error OrderStatusHasBeenExpired =>
        Error.Validation("Order.StatusHasBeenExpired", OrderCannotBeConfirmedAfterExpirationRule.Message);

    public static Error OrderStatusAlreadyConfirmed =>
        Error.Validation("Order.StatusAlreadyConfirmed", OrderCannotBeConfirmedWhenOrderStatusIsConfirmedRule.Message);

    public static Error OrderStatusAlreadyExpired =>
        Error.Validation("Order.StatusAlreadyExpired", OrderCannotBeExpiredWhenOrderStatusIsExpiredRule.Message);

    public static Error ItemIsOutOfStock =>
        Error.Validation("Order.ItemIsOutOfStock", OrderCannotBePlacedWhenItemIsOutOfStockRule.Message);

    public static Error AmountRequestedGreaterThanActualStock =>
        Error.Validation("Order.AmountRequestedGreaterThanActualStock", OrderItemAmountCannotBeGreaterThanItemActualAmountRule.Message);

    public static Error OrderStatusIsCompleted =>
        Error.Validation("Order.StatusIsCompleted", OrderCannotBeExpiredAfterCompletationRule.Message);

    public static Error CannotPayedWhenStatusIsExpired =>
        Error.Validation("Order.StatusIsExpired", OrderCannotBePayedWhenOrderStatusIsExpiredRule.Message);

    public static Error CannotBePayedWhenStatusIsNotConfirmed =>
        Error.Validation("Order.StatusIsNotConfirmed", OrderCannotBePayedWhenOrderStatusIsNotConfirmedRule.Message);

    public static Error CannotBeConfirmedWhenOrderStatusIsPayed =>
        Error.Validation("Order.StatusIsPayed", OrderCannotBeConfirmedWhenOrderStatusIsPayedRule.Message);

    public static Error CannotBeConfirmedAfterCompletation =>
        Error.Validation("Order.StatusIsCompleted", OrderCannotBeConfirmedAfterCompletationRule.Message);
}
