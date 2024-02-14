using System;
using BuildingBlocks.Domain;
using Catalog.Domain.Products.Errors;
using Catalog.Domain.Products.ValueObjects;
using ErrorOr;

namespace Catalog.Domain.Products.Rules;
internal sealed class ProductCannotBeSoldWhenProductIsOutOfStockRule : IBusinessRule
{
    private readonly StockStatus _stockStatus;

    public ProductCannotBeSoldWhenProductIsOutOfStockRule(StockStatus stockStatus)
    {
        _stockStatus = stockStatus;
    }

    public Error Error => ProductErrorCodes.ProductOutOfStock;

    public bool IsBroken() => _stockStatus == StockStatus.OutOfStock;

    public static string Message => "Product cannot be sold when product is out of stock";
}
