using BuildingBlocks.Application.Queries;
using Catalog.Domain.Products;
using Catalog.Domain.Products.Rules;
using ErrorOr;

namespace Catalog.Application.Products.GetProductsByProductType;
internal sealed class GetProductsByProductTypeQueryHandler

    : IQueryRequestHandler<GetProductsByProductTypeQuery, ErrorOr<IReadOnlyList<ProductResponse>>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsByProductTypeQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<IReadOnlyList<ProductResponse>>> Handle(GetProductsByProductTypeQuery query, CancellationToken cancellationToken)
    {
        ProductType productType = ProductType.Create(query.ProductType);

        List<Product>? products = await _productRepository.GetByProductTypeAsync(productType); 
    
        if (products is null)
        {
            return Error.NotFound("Products.NotFound", $"Products with the product type {query.ProductType} were not found");
        }

        List<ProductResponse> response = products.ConvertAll(
                product => new ProductResponse
            (
                Id: product.Id.Value,
                SellerId: product.SellerId,
                Name: product.Name,
                Price: product.Price,
                Description: product.Description,
                Size: product.Size,
                Color: product.Color,
                ProductType: product.ProductType.Value,
                Tags: product.Tags.Select(tag => tag.Value).ToList(),
                InStock: product.InStock,
                CreatedDateTime: product.CreatedDateTime,
                UpdatedDateTime: product.UpdatedDateTime
            ));


        
        return response.AsReadOnly();
    }

}
