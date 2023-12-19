using BuildingBlocks.Domain;
using Catalog.Domain.Products.Errors;
using ErrorOr;

namespace Catalog.Domain.Products.Rules;

internal sealed class ProductCannotBeSoldWhenAmountOfProductsInBuyingRequestIsGreaterThanActualInStockRule : IBusinessRule
{
    private readonly int _inStock;
    private readonly int _amountOfProductsInBuyingRequest;

    public ProductCannotBeSoldWhenAmountOfProductsInBuyingRequestIsGreaterThanActualInStockRule(int amountOfProductsInBuyingRequest, int inStock)
    {
        _amountOfProductsInBuyingRequest = amountOfProductsInBuyingRequest;
        _inStock = inStock;

    }


    public Error Error => ProductErrors.AmountOfProductsRequestedGreaterThanAmountOfProductsInStock;

    public bool IsBroken() => _amountOfProductsInBuyingRequest > _inStock;

    public static string Message => "Product cannot be sold when amount of products in buying request is greater than actual in stock amount";
}
