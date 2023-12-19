using Catalog.Domain.Products.Rules;

namespace Catalog.Domain.Products;
public interface IProductRepository
{
    Task AddAsync(Product product);

    Task RemoveAsync(ProductId productId);

    Task UpdateAsync(Product product);

    Task<List<Product>?> GetBySellerAsync(Guid sellerId);
    Task<Product?> GetByIdAsync(ProductId productId);      

    Task<List<Product>?> GetByNameAsync(string name);

    Task<List<Product>?> GetByTagAsync(string tag);

    Task<List<Product>?> GetByProductTypeAsync(ProductType productType); 
}
