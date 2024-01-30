using Catalog.Application.Common;
using Catalog.Domain.Products;
using Catalog.Domain.Products.Errors;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.ModifyProduct.ModifyProductType;

internal sealed class ModifyProductTypeCommandHandler : ICommandRequestHandler<ModifyProductTypeCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;
    private readonly AuthorizationService _authorizationService;

    public ModifyProductTypeCommandHandler(IProductRepository productRepository, AuthorizationService authorizationService)
    {
        _productRepository = productRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Unit>> Handle(ModifyProductTypeCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(ProductId.Create(request.ProductId));
    
        if (product is null)
        {
            return ProductErrorCodes.NotFound;
        }

        var authorizationService = _authorizationService.IsUserAuthorized(product.SellerId);

        if (authorizationService.IsError)
        {
            return ProductErrorCodes.CannotAccessToContent;
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