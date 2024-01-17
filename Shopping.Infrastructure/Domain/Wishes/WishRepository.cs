using Microsoft.EntityFrameworkCore;
using Shopping.Domain.Wishes;

namespace Shopping.Infrastructure.Domain.Wishes;

internal sealed class WishRepository : IWishRepository
{
    private readonly ShoppingDbContext _dbContext;

    public WishRepository(ShoppingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Wish wish)
    {
        await InsertWish(wish);
        await InsertWishItems(wish);
    }

    public async Task DeleteAsync(Wish wish)
    {
        await _dbContext
            .Wishes
            .Where(x => x.Id == wish.Id)
            .ExecuteDeleteAsync();
    }

    public async Task<Wish?> GetByIdAsync(WishId wishId)
    {
        return await _dbContext
            .Wishes
            .Where(x => x.Id == wishId)
            .SingleOrDefaultAsync();
    }

    public async Task UpdateAsync(Wish wish)
    {
        await _dbContext
            .Wishes
            .Where(x => x.Id == wish.Id)
            .ExecuteUpdateAsync(setters =>
            setters
                .SetProperty(s => s.Id, wish.Id)
                .SetProperty(s => s.CustomerId, wish.CustomerId)
                .SetProperty(s => s.Items, wish.Items)
                .SetProperty(s => s.Name, wish.Name)
                .SetProperty(s => s.IsPrivate, wish.IsPrivate)
                .SetProperty(s => s.CreatedOn, wish.CreatedOn));
    }

    private async Task InsertWish(Wish wish)
    {
        await _dbContext
            .Database
            .ExecuteSqlRawAsync(
            "INSERT INTO shopping.Wishes (WishId, " +
            "CustomerId, " +
            "Name, " +
            "IsPrivate, " +
            "CreatedOn) " +
            "VALUES({0}, {1}, {2}, {3}, {4})",
            wish.Id.Value,
            wish.CustomerId,
            wish.Name,
            wish.IsPrivate,
            wish.CreatedOn);
    }

    private async Task InsertWishItems(Wish wish)
    {
        foreach (var item in wish.Items)
        {
            await _dbContext
            .Database
            .ExecuteSqlRawAsync(
            "INSERT INTO shopping.WishItems (WishId, ItemId) " +
            "VALUES ({0}, {1})",
            wish.Id.Value, item.Value);
        }
    }
}
