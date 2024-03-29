using Catalog.Application.Common;
using Catalog.Domain.Products;
using Catalog.Domain.Products.Errors;
using Catalog.Domain.Products.ValueObjects;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.ModifyProduct.ModifyTag;

internal sealed class ModifyTagCommandHandler : ICommandRequestHandler<ModifyTagCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;
    private readonly IAuthorizationService _authorizationService;

    public ModifyTagCommandHandler(IProductRepository productRepository, IAuthorizationService authorizationService)
    {
        _productRepository = productRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Unit>> Handle(ModifyTagCommand request, CancellationToken cancellationToken)
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

        List<Tag> tags = request.Tags.ConvertAll(tag => Tag.Create(tag));

        Product update = Product.Update(
            product.Id, 
            product.SellerId, 
            product.Name, 
            product.Price,
            product.Description, 
            product.Sizes,
            product.ProductType, 
            tags,
            product.InStock, 
            product.CreatedDateTime, 
            DateTime.UtcNow, 
            product.Colors);

        await _productRepository.UpdateAsync(update);

        return Unit.Value;
    }
}