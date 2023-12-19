using BuildingBlocks.Application.Commands;
using Catalog.Domain.Products;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.ModifyProduct.ModifyColor;
internal sealed class ModifyColorCommandHandler 
    : ICommandRequestHandler<ModifyColorCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;

    public ModifyColorCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(ModifyColorCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(ProductId.Create(request.ProductId));
    
        if (product is null)
        {
            return Error.NotFound("Product.NotFound", "Product was not found");
        }

        var update = Product.Update(product.Id, 
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
            request.Color);

        await _productRepository.UpdateAsync(update);

        return Unit.Value;
    }
}