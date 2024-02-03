using Catalog.Application.Common;
using Catalog.Domain.Products;
using Catalog.Domain.Products.Errors;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.RemoveProduct;

internal sealed class RemoveProductCommandHandler : ICommandRequestHandler<RemoveProductCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;
    private readonly IAuthorizationService _authorizationService;

    public RemoveProductCommandHandler(IProductRepository productRepository, IAuthorizationService authorizationService)
    {
        _productRepository = productRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Unit>> Handle(RemoveProductCommand command, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(ProductId.Create(command.Id));

        if (product is null)
        {
            return ProductErrorCodes.NotFound;
        }

        var authorizationService = _authorizationService.IsUserAuthorized(product.SellerId);

        if (authorizationService.IsError && _authorizationService.IsAdmin() is false)
        {
            return ProductErrorCodes.CannotAccessToContent;
        }

        product.Remove();

        await _productRepository.RemoveAsync(product, cancellationToken);

        return Unit.Value;
    }

}
