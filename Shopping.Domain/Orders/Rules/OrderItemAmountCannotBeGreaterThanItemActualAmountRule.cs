using BuildingBlocks.Domain;
using ErrorOr;
using Shopping.Domain.Orders.Errors;

namespace Shopping.Domain.Orders.Rules;

internal sealed class OrderItemAmountCannotBeGreaterThanItemActualAmountRule : IBusinessRule
{
    private readonly int _amountRequested;
    private readonly int _actualStock;

    public OrderItemAmountCannotBeGreaterThanItemActualAmountRule(int amountRequested, int actualStock)
    {
        _amountRequested = amountRequested;
        _actualStock = actualStock;
    }

    public Error Error => OrderErrorCodes.AmountRequestedGreaterThanActualStock;

    public bool IsBroken() => _amountRequested > _actualStock;

    public static string Message => "Order item amount cannot be greater than item actual amount";
}
