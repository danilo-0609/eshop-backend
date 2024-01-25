using Catalog.Application.Common;
using Catalog.Domain.Products;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.ModifyProduct.ModifyName;

internal sealed class ModifyNameCommandHandler : ICommandRequestHandler<ModifyNameCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;

    public ModifyNameCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(ModifyNameCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(ProductId.Create(request.ProductId));
    
        if (product is null)
        {
            return Error.NotFound("Product.NotFound", "Product was not found");
        }

        var update = Product.Update(
            product.Id, 
            product.SellerId, 
            request.Name, 
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