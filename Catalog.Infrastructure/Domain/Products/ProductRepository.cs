using Catalog.Domain.Products;
using Catalog.Infrastructure.Outbox;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
        await UpdateProduct(product);
        await InsertTags(product.Tags, product.Id);
        await InsertUpdateDomainEvent(product);
    }

    private async Task UpdateProduct(Product product)
    {
        await _dbContext.Database.ExecuteSqlRawAsync(
     @"UPDATE [Eshop.Db].[catalog].[Products] 
      SET SellerId = @SellerId,
          Name = @Name,
          Price = @Price,
          Description = @Description,
          Size = @Size,
          Color = @Color,
          ProductTypeValue = @ProductTypeValue,
          InStock = @InStock,
          IsActive = @IsActive,
          CreatedDateTime = @CreatedDateTime,
          UpdatedDateTime = @UpdatedDateTime
      WHERE ProductId = @ProductId",
     new SqlParameter("@SellerId", product.SellerId),
     new SqlParameter("@Name", product.Name),
     new SqlParameter("@Price", product.Price),
     new SqlParameter("@Description", product.Description),
     new SqlParameter("@Size", product.Size),
     new SqlParameter("@Color", product.Color),
     new SqlParameter("@ProductTypeValue", product.ProductType.Value),
     new SqlParameter("@InStock", product.InStock),
     new SqlParameter("@IsActive", product.IsActive),
     new SqlParameter("@CreatedDateTime", product.CreatedDateTime),
     new SqlParameter("@UpdatedDateTime", product.UpdatedDateTime),
     new SqlParameter("@ProductId", product.Id.Value));

    }

    private async Task InsertTags(List<Tag> tags, ProductId productId)
    {
        List<string> tagsList = await _dbContext
            .Products
            .Where(q => q.Id == productId)
            .SelectMany(r => r.Tags
                .Select(r => r.Value))
            .ToListAsync();

        foreach (var tag in tags)
        {
            if (!tagsList.Contains(tag.Value))
            {
                await _dbContext
                    .Database
                    .ExecuteSqlRawAsync(
                    """
                    INSERT INTO catalog.Tags (ProductId, Value)
                    VALUES ({0}, {1});
                    """,
                    productId.Value, tag.Value);
            }
        }
    }

    private async Task InsertUpdateDomainEvent(Product product)
    {
        var domainEvents = product.GetDomainEvents();

        List<CatalogOutboxMessage> outboxMessage = domainEvents
            .Select(domainEvent => new CatalogOutboxMessage
            {
                Id = domainEvent.DomainEventId,
                Type = domainEvent.GetType().Name,
                OcurredOnUtc = domainEvent.OcurredOn,
                Content = JsonConvert.SerializeObject(
                domainEvent,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                })
            }).ToList();

        product.ClearDomainEvents();

        await _dbContext.CatalogOutboxMessages.AddRangeAsync(outboxMessage);
    }
}


