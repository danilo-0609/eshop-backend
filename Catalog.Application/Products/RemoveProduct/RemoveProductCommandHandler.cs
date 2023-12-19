using BuildingBlocks.Application.Commands;
using Catalog.Domain.Products;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.RemoveProduct;
internal sealed class RemoveProductCommandHandler : ICommandRequestHandler<RemoveProductCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;

    public RemoveProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }


    public async Task<ErrorOr<Unit>> Handle(RemoveProductCommand command, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(ProductId.Create(command.Id));

        if (product is null)
        {
            return Error.NotFound("Product.NotFound", "Product was not found");
        }

        await _productRepository.RemoveAsync(product.Id);

        return Unit.Value;
    }

}
