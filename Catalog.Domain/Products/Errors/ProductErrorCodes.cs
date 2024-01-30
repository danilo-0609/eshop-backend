using Catalog.Domain.Products.Rules;
using ErrorOr;

namespace Catalog.Domain.Products.Errors;

public static class ProductErrorCodes
{
    public static Error ProductOutOfStock =>
        Error.Validation("Product.OutOfStock", ProductCannotBeSoldWhenProductIsOutOfStockRule.Message);
    
    public static Error AmountOfProductsRequestedGreaterThanAmountOfProductsInStock => 
        Error.Validation("Product.AmountOfProductsRequestedGreaterThanPossible", ProductCannotBeSoldWhenAmountOfProductsInBuyingRequestIsGreaterThanActualInStockRule.Message);

    public static Error NotFound =>
        Error.NotFound("Product.NotFound", "Product was not found");

    public static Error CannotAccessToContent =>
        Error.Unauthorized("Product.CannotAccess", "Cannot access to this content");
}

