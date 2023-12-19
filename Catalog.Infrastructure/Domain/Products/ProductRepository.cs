using System.Linq;
using Catalog.Domain.Products;
using Catalog.Domain.Products.Rules;
using Catalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Domain.Products;
internal sealed class ProductRepository : IProductRepository
{
    private readonly CatalogDbContext _dbContext;

    public ProductRepository(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Product product)
    {
        await _dbContext.Products.AddAsync(product);
    }

    public async Task<Product?> GetByIdAsync(ProductId productId)
    {
        return await _dbContext
            .Products
            .Where(t => t.Id == productId)
            .SingleOrDefaultAsync();
    }

    public async Task<List<Product>?> GetByNameAsync(string name)
    {
        List<Product> products = await _dbContext
            .Products
            .Where(t => t.Name == name)
            .ToListAsync();

        if (products.Count == 0)
        {
            return null;
        }

        return products;
    }

    public async Task<List<Product>?> GetByProductTypeAsync(ProductType productType)
    {
        List<Product> products = await _dbContext
            .Products
            .Where(t => t.ProductType == productType)
            .ToListAsync();

        if (products.Count == 0)
        {
            return null;
        }

        return products;
    }

    public async Task<List<Product>?> GetBySellerAsync(Guid sellerId)
    {
        List<Product> products = await _dbContext
            .Products
            .Where(t => t.SellerId == sellerId)
            .ToListAsync();

        if (products.Count == 0)
        {
            return null;
        }

        return products;
    }

    public async Task<List<Product>?> GetByTagAsync(string tag)
    {
        var tagValueObject = Tag.Create(tag);

        List<Product> products = await _dbContext
            .Products
            .Where(f => f.Tags.Contains(tagValueObject))
            .ToListAsync();

        if (products.Count == 0)
        {
            return null;
        }

        return products;
    }

    public async Task RemoveAsync(ProductId productId)
    {
        await _dbContext
            .Products
            .Where(t => t.Id == productId)
            .ExecuteDeleteAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        await _dbContext
            .Products
            .Where(g => g.Id == product.Id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(b => b.Id, product.Id)
                .SetProperty(b => b.SellerId, product.SellerId)
                .SetProperty(b => b.Name, product.Name)
                .SetProperty(b => b.Price, product.Price)
                .SetProperty(b => b.Size, product.Size)
                .SetProperty(b => b.Color, product.Color)
                .SetProperty(b => b.ProductType, product.ProductType)
                .SetProperty(b => b.Tags, product.Tags)
                .SetProperty(b => b.InStock, product.InStock)
                .SetProperty(b => b.IsActive, product.IsActive)
                .SetProperty(b => b.CreatedDateTime, product.CreatedDateTime)
                .SetProperty(b => b.UpdatedDateTime, product.UpdatedDateTime)
                .SetProperty(b => b.ExpiredDateTime, product.ExpiredDateTime));
    }
}


