using ErrorOr;
using MediatR;

namespace Catalog.Domain.Products.Services;

public sealed class SellProductService
{
    private readonly IProductRepository _productRepository;

    public SellProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Unit>> TrySellProduct(
        ProductId productId,
        Guid orderId,
        int amountOfProducts)
    {
        Product? product = await _productRepository.GetByIdAsync(productId);

        if (product is null)
        {
            return Error.NotFound("Product.NotFound", "Product was not found");
        }

        var sellTried = product.Sell(amountOfProducts, orderId);

        if (sellTried.IsError)
        {
            return sellTried.FirstError;
        }

        Product update = Product.Update(
            product.Id,
            product.SellerId,
            product.Name,
            product.Price,
            product.Description,
            product.Size,
            product.ProductType,
            product.Tags,
            product.InStock,
            product.CreatedDateTime,
            DateTime.UtcNow,
            product.Color);

        await _productRepository.UpdateAsync(update);

        return Unit.Value;
    }
}
