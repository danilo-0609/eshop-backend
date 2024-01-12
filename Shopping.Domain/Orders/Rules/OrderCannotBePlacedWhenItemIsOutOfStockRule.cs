using BuildingBlocks.Domain;
using ErrorOr;
using Shopping.Domain.Items;
using Shopping.Domain.Orders.Errors;

namespace Shopping.Domain.Orders.Rules;

internal sealed class OrderCannotBePlacedWhenItemIsOutOfStockRule : IBusinessRule
{
    private readonly StockStatus _stockStatus;

    public OrderCannotBePlacedWhenItemIsOutOfStockRule(StockStatus stockStatus)
    {
        _stockStatus = stockStatus;
    }

    public Error Error => OrderErrors.ItemIsOutOfStock;

    public bool IsBroken() => _stockStatus == StockStatus.OutOfStock;

    public static string Message => "Order cannot be placed when item is out of stock";
}
