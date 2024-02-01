using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Shopping.Domain.Items;

namespace Shopping.Infrastructure.Domain.Items;

internal sealed class ItemRepository : IItemRepository
{
    private readonly ShoppingDbContext _dbContext;

    public ItemRepository(ShoppingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Item item)
    {
        await _dbContext
            .Database
            .ExecuteSqlRawAsync(
            """
            INSERT INTO shopping.Items (
            ItemId, 
            Name, 
            SellerId, 
            Price, 
            InStock, 
            StockStatus, 
            CreatedOn, 
            UpdatedOn)
            VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7});
            """,
            item.Id.Value,
            item.Name,
            item.SellerId,
            item.Price,
            item.InStock,
            item.StockStatus.Value,
            item.CreatedOn,
            item.UpdatedOn);
    }

    public async Task DeleteAsync(ItemId id)
    {
        await _dbContext
            .Items
            .Where(d => d.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<Item?> GetByIdAsync(ItemId id)
    {
        return await _dbContext
            .Items
            .Where(d => d.Id == id)
            .SingleOrDefaultAsync();
    }

    public async Task UpdateAsync(Item item)
    {
        await _dbContext
            .Database
            .ExecuteSqlRawAsync(
            @"
            UPDATE shopping.Items
            SET Name = @Name,
                SellerId = @SellerId,
                Price = @Price,
                InStock = @InStock,
                StockStatus = @StockStatus,
                CreatedOn = @CreatedOn,
                UpdatedOn = @UpdatedOn
            WHERE ItemId = @ItemId
            ",
            new SqlParameter("@Name", item.Name),
            new SqlParameter("@SellerId", item.SellerId),
            new SqlParameter("@Price", item.Price),
            new SqlParameter("@InStock", item.InStock),
            new SqlParameter("@StockStatus", item.StockStatus.Value),
            new SqlParameter("@CreatedOn", item.CreatedOn),
            new SqlParameter("@UpdatedOn", item.UpdatedOn),
            new SqlParameter("@ItemId", item.Id.Value));
    }

    public Guid? GetSellerIdAsync(ItemId itemId)
    {
        return _dbContext
            .Items
            .Where(r => r.Id == itemId)
            .Select(f => f.SellerId)
            .FirstOrDefault();
    }
}

