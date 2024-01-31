using Catalog.Application.Common;
using Catalog.Domain.Products;
using Catalog.Domain.Products.Errors;
using ErrorOr;

namespace Catalog.Application.Products.GetProductById;

internal sealed class GetProductByIdQueryHandler : IQueryRequestHandler<GetProductByIdQuery, ErrorOr<ProductResponse>>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(ProductId.Create(request.ProductId));

        if (product is null)
        {
            return ProductErrorCodes.NotFound;
        }

        return new ProductResponse(
            product.Id.Value,
            product.SellerId,
            product.Name,
            product.Price,
            product.Description,
            product.Size,
            product.Color,
            product.ProductType.Value,
            product.Tags.Select(r => r.Value).ToList(),
            product.InStock,
            product.CreatedDateTime,
            product.UpdatedDateTime);
    }
}
