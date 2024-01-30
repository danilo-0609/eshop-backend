using Catalog.Application.Common;
using Catalog.Domain.Products;
using Catalog.Domain.Products.Errors;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.ModifyProduct.ModifyColor;

internal sealed class ModifyColorCommandHandler : ICommandRequestHandler<ModifyColorCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;
    private readonly AuthorizationService _authorizationService;

    public ModifyColorCommandHandler(IProductRepository productRepository, AuthorizationService authorizationService)
    {
        _productRepository = productRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Unit>> Handle(ModifyColorCommand request, CancellationToken cancellationToken)
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