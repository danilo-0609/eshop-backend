using BuildingBlocks.Domain;
using ErrorOr;
using Shopping.Domain.Common;
using Shopping.Domain.Items;
using Shopping.Domain.Payments.Errors;

namespace Shopping.Domain.Payments.Rules;

internal sealed class PayCannotBeMadeWhenItemAmountRequestedIsGreaterThanItemCurrentAmountRule : IBusinessRule
{
    private readonly int _amountRequested;
    private readonly int _actualAmount

    public PayCannotBeMadeWhenItemAmountRequestedIsGreaterThanItemCurrentAmountRule(int amountRequested, int actualAmount)
    {
        _amountRequested = amountRequested;
        _actualAmount = actualAmount;
    }

    public Error Error => PaymentErrors.ItemAmountRequestedIsGreaterThanItemCurrentAmount;

    public bool IsBroken() => _actualAmount < _amountRequested;

    public static string Message => "Pay cannot be made when item amount requested is greater than item current amount";
}
