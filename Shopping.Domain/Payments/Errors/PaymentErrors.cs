using ErrorOr;
using Shopping.Domain.Payments.Rules;

namespace Shopping.Domain.Payments.Errors;

public static class PaymentErrors
{
    public static Error ItemAmountRequestedIsGreaterThanItemCurrentAmount =>
        Error.Validation("Payment.AmountRequestedGreaterThanCurrentAmount", PayCannotBeMadeWhenItemAmountRequestedIsGreaterThanItemCurrentAmountRule.Message);

    public static Error ItemIsOutOfStock => 
        Error.Validation("Payment.ItemIsOutOfStock", PayCannotBeMadeWhenItemIsOutOfStockRule.Message);
}
