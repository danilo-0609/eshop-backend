using BuildingBlocks.Domain;
using Catalog.Domain.Products.Errors;
using Catalog.Domain.Products.ValueObjects;
using ErrorOr;

namespace Catalog.Domain.Products.Rules;

internal sealed class ProductCannotBePublishedWithNoStockRule : IBusinessRule
{
    private readonly StockStatus _stockStatus;

    public ProductCannotBePublishedWithNoStockRule(StockStatus stockStatus)
    {
        _stockStatus = stockStatus;
    }

    public Error Error => ProductErrorCodes.CannotPublishWithNoStock;

    public bool IsBroken() => _stockStatus == StockStatus.OutOfStock;

    public static string Message => "Product cannot be published with no stock";
}
