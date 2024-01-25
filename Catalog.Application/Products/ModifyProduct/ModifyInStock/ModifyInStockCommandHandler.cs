using Catalog.Application.Common;   
using Catalog.Domain.Products;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.ModifyProduct.ModifyInStock;

internal sealed class ModifyInStockCommandHandler : ICommandRequestHandler<ModifyInStockCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;

    public ModifyInStockCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(ModifyInStockCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(ProductId.Create(request.ProductId));
    
        if (product is null)
        {
            return Error.NotFound("Product.NotFound", "Product was not found");
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
            request.InStock, 
            product.CreatedDateTime, 
            DateTime.UtcNow, 
            product.Color);

        await _productRepository.UpdateAsync(update);
    
        return Unit.Value;
    }
}