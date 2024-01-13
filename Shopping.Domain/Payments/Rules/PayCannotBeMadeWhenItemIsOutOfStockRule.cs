using BuildingBlocks.Domain;
using ErrorOr;
using Shopping.Domain.Items;
using Shopping.Domain.Payments.Errors;

namespace Shopping.Domain.Payments.Rules;

internal sealed class PayCannotBeMadeWhenItemIsOutOfStockRule : IBusinessRule
{
    private readonly StockStatus _actualStockStatus;

    public PayCannotBeMadeWhenItemIsOutOfStockRule(StockStatus stockStatus)
    {
        _actualStockStatus = stockStatus;
    }

    public Error Error => PaymentErrors.ItemIsOutOfStock;

    public bool IsBroken() => _actualStockStatus == StockStatus.OutOfStock;

    public static string Message => "Pay cannot be made when item is out of stock";
}
