using Catalog.Application.Common;
using Catalog.Domain.Products;
using Catalog.Domain.Products.Errors;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.ModifyProduct.ModifyDescription;

internal sealed class ModifyDescriptionCommandHandler : ICommandRequestHandler<ModifyDescriptionCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;
    private readonly IAuthorizationService _authorizationService;

    public ModifyDescriptionCommandHandler(IProductRepository productRepository, IAuthorizationService authorizationService)
    {
        _productRepository = productRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Unit>> Handle(ModifyDescriptionCommand request, CancellationToken cancellationToken)
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
            request.Description, 
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