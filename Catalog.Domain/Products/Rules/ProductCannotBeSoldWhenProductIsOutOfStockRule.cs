using System;
using BuildingBlocks.Domain;
using Catalog.Domain.Products.Errors;
using ErrorOr;

namespace Catalog.Domain.Products.Rules;
internal sealed class ProductCannotBeSoldWhenProductIsOutOfStockRule : IBusinessRule
{
    private readonly int _inStock;

    public ProductCannotBeSoldWhenProductIsOutOfStockRule(int inStock)
    {
        _inStock = inStock;
    }

    public Error Error => ProductErrorCodes.ProductOutOfStock;

    public bool IsBroken() => _inStock == 0;

    public static string Message => "Product cannot be sold when product is out of stock";
}
