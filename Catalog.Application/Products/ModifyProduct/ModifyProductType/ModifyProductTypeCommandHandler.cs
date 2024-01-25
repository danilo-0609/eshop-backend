using Catalog.Application.Common;
using Catalog.Domain.Products;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.ModifyProduct.ModifyProductType;

internal sealed class ModifyProductTypeCommandHandler : ICommandRequestHandler<ModifyProductTypeCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;

    public ModifyProductTypeCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(ModifyProductTypeCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(ProductId.Create(request.ProductId));
    
        if (product is null)
        {
            return Error.NotFound("Product.NotFound", "Product was not found");
        }

        ProductType productType = ProductType.Create(request.ProductType);

        Product update = Product.Update(product.Id, 
            product.SellerId, 
            product.Name, 
            product.Price,
            product.Description, 
            product.Size,
            productType, 
            product.Tags,
            product.InStock, 
            product.CreatedDateTime, 
            DateTime.UtcNow, 
            product.Color);

        await _productRepository.UpdateAsync(update);

        return Unit.Value;
    }
}