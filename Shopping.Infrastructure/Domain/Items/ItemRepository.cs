using Microsoft.EntityFrameworkCore;
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
            .Items
            .AddAsync(item);
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
            .Items
            .Where(d => d.Id == item.Id)
            .ExecuteUpdateAsync(setters =>
            setters
                .SetProperty(x => x.Id, item.Id)
                .SetProperty(x => x.Name, item.Name)
                .SetProperty(x => x.SellerId, item.SellerId)
                .SetProperty(x => x.Price, item.Price)
                .SetProperty(x => x.InStock, item.InStock)
                .SetProperty(x => x.StockStatus, item.StockStatus)
                .SetProperty(x => x.CreatedOn, item.CreatedOn)
                .SetProperty(x => x.UpdatedOn, item.UpdatedOn));
    }
}

