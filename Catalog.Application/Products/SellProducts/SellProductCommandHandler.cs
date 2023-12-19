using BuildingBlocks.Application.Commands;
using Catalog.Domain.Products;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.SellProducts;
internal sealed class SellProductCommandHandler
    : ICommandRequestHandler<SellProductCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;

    public SellProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(SellProductCommand command, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(ProductId.Create(command.ProductId));

        if (product is null)
        {
            return Error.NotFound("Product.NotFound", "Product was not found");
        }

        var sellTried = product.Sell(command.AmountOfProducts);

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
