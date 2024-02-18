using Catalog.Domain.Products;
using Catalog.Infrastructure.Domain.Products;
using Microsoft.Extensions.Caching.Memory;

namespace Catalog.Infrastructure.Cache;

internal sealed class CachedProductRepository : IProductRepository
{
    private readonly IProductRepository _decorated;
    private readonly IMemoryCache _memoryCache;

    public CachedProductRepository(IProductRepository decorated, IMemoryCache memoryCache)
    {
        _decorated = decorated;
        _memoryCache = memoryCache;
    }

    public async Task AddAsync(Product product)
    {
        await _decorated.AddAsync(product);
    }

    public Task<bool> ExistsAsync(ProductId productId, CancellationToken cancellationToken)
    {
        return _decorated.ExistsAsync(productId, cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(ProductId productId)
    {
        string key = $"product-{productId.Value}";

        return await _memoryCache.GetOrCreateAsync(
            key,
            async entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                return await _decorated.GetByIdAsync(productId);
            });
    }

    public async Task<List<Product>?> GetByNameAsync(string name)
    {
        string key = $"product-{name}";

        return await _memoryCache.GetOrCreateAsync(
            key,
            async entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                return await _decorated.GetByNameAsync(name);
            });
    }

    public async Task<List<Product>?> GetByProductTypeAsync(ProductType productType)
    {
        string key = $"product-{productType.Value}";

        return await _memoryCache.GetOrCreateAsync(
            key,
            async entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                return await _decorated.GetByProductTypeAsync(productType);
            });
    }

    public async Task<List<Product>?> GetBySellerAsync(Guid sellerId)
    {
        string key = $"product-{sellerId}";

        return await _memoryCache.GetOrCreateAsync(
            key,
            async entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                return await _decorated.GetBySellerAsync(sellerId);
            });
    }

    public async Task<List<Product>?> GetByTagAsync(string tag)
    {
        string key = $"product-{tag}";

        return await _memoryCache.GetOrCreateAsync(
            key,
            async entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                return await _decorated.GetByTagAsync(tag);
            });
    }

    public async Task RemoveAsync(Product product, CancellationToken cancellationToken)
    {
        await _decorated.RemoveAsync(product, cancellationToken);
    }

    public async Task UpdateAsync(Product product)
    {
        await _decorated.UpdateAsync(product);
    }
}
