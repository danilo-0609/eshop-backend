using Catalog.Application.Common;
using Catalog.Domain.Products;
using Catalog.Domain.Products.Errors;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.ModifyProduct.ModifyPrice;

internal sealed class ModifyPriceCommandHandler : ICommandRequestHandler<ModifyPriceCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;
    private readonly IAuthorizationService _authorizationService;

    public ModifyPriceCommandHandler(IProductRepository productRepository, IAuthorizationService authorizationService)
    {
        _productRepository = productRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Unit>> Handle(ModifyPriceCommand request, CancellationToken cancellationToken)
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

        Product update = Product.Update(product.Id, 
            product.SellerId, 
            product.Name, 
            request.Price,
            product.Description, 
            product.Sizes,
            product.ProductType, 
            product.Tags,
            product.InStock, 
            product.CreatedDateTime, 
            DateTime.UtcNow, 
            product.Colors);

        await _productRepository.UpdateAsync(update);

        return Unit.Value;
    }
}