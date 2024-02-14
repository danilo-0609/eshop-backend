using Catalog.Application.Common;
using Catalog.Domain.Products;
using Catalog.Domain.Products.Errors;
using Catalog.Domain.Products.ValueObjects;
using ErrorOr;
using MediatR;
using System.Drawing;
using Size = Catalog.Domain.Products.ValueObjects.Size;

namespace Catalog.Application.Products.ModifyProduct.ModifySize;
internal sealed class ModifySizeCommandHandler : ICommandRequestHandler<ModifySizeCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;
    private readonly IAuthorizationService _authorizationService;

    public ModifySizeCommandHandler(IProductRepository productRepository, IAuthorizationService authorizationService)
    {
        _productRepository = productRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Unit>> Handle(ModifySizeCommand request, CancellationToken cancellationToken)
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

        List<Size> sizes = request.Sizes.ConvertAll(size => new Size(size));

        Product update = Product.Update(
            product.Id, 
            product.SellerId, 
            product.Name, 
            product.Price,
            product.Description, 
            sizes,
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