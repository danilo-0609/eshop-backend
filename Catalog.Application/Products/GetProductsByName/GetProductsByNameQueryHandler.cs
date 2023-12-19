using BuildingBlocks.Application.Queries;
using Catalog.Domain.Products;
using ErrorOr;

namespace Catalog.Application.Products.GetProductsByName;
internal sealed class GetProductsByNameQueryHandler : 
    IQueryRequestHandler<GetProductsByNameQuery, ErrorOr<IReadOnlyList<ProductResponse>>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsByNameQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<IReadOnlyList<ProductResponse>>> Handle(GetProductsByNameQuery query, CancellationToken cancellationToken)
    {
        List<Product>? products = await _productRepository.GetByNameAsync(query.Name);
    
        if (products is null)
        {
            return Error.NotFound("Products.NotFound", $"Products with the name {query.Name} were not found");
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
