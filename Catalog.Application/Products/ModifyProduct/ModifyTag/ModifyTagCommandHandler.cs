using BuildingBlocks.Application.Commands;
using Catalog.Domain.Products;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.ModifyProduct.ModifyTag;
internal sealed class ModifyTagCommandHandler : ICommandRequestHandler<ModifyTagCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;

    public ModifyTagCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(ModifyTagCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(ProductId.Create(request.ProductId));
    
        if (product is null)
        {
            return Error.NotFound("Product.NotFound", "Product was not found");
        }
    
        List<Tag> tags = request.Tags.ConvertAll(tag => Tag.Create(tag));

        Product update = Product.Update(
            product.Id, 
            product.SellerId, 
            product.Name, 
            product.Price,
            product.Description, 
            product.Size,
            product.ProductType, 
            tags,
            product.InStock, 
            product.CreatedDateTime, 
            DateTime.UtcNow, 
            product.Color);

        await _productRepository.UpdateAsync(update);

        return Unit.Value;
    }
}