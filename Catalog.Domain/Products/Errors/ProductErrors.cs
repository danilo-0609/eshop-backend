using System.Reflection.Metadata;
using Catalog.Domain.Products.Rules;
using ErrorOr;

namespace Catalog.Domain.Products.Errors;

public static class ProductErrors
{
    public static Error ProductOutOfStock =>
        Error.Validation("Product.OutOfStock", ProductCannotBeSoldWhenProductIsOutOfStockRule.Message);
    
    public static Error AmountOfProductsRequestedGreaterThanAmountOfProductsInStock => 
        Error.Validation("Product.AmountOfProductsRequestedGreaterThanPossible", ProductCannotBeSoldWhenAmountOfProductsInBuyingRequestIsGreaterThanActualInStockRule.Message);
}

