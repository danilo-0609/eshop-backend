using BuildingBlocks.Application.Commands;
using Catalog.Domain.Products;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.ModifyProduct.ModifyDescription;
internal sealed class ModifyDescriptionCommandHandler 
    : ICommandRequestHandler<ModifyDescriptionCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;

    public ModifyDescriptionCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(ModifyDescriptionCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(ProductId.Create(request.ProductId));

        if (product is null)
        {
            return Error.NotFound("Product.NotFound", "Product with the id provided was not found");
        } 

        var update = Product.Update(product.Id, 
            product.SellerId, 
            product.Name, 
            product.Price,
            request.Description, 
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