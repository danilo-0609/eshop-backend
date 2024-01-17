using Microsoft.EntityFrameworkCore;
using Shopping.Domain.Basket;

namespace Shopping.Infrastructure.Domain.Baskets;

internal sealed class BasketRepository : IBasketRepository
{
    private readonly ShoppingDbContext _context;

    public BasketRepository(ShoppingDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Basket basket)
    {
        await InsertBasket(basket);
        await InsertBasketItems(basket);
    }

    public async Task DeleteAsync(Basket basket)
    {
        await _context
            .Baskets
            .Where(d => d.Id == basket.Id)
            .ExecuteDeleteAsync();
    }

    public async Task<Basket?> GetByIdAsync(BasketId basketId)
    {
        return await _context
            .Baskets
            .Where(d => d.Id == basketId)
            .SingleOrDefaultAsync();
    }

    public async Task UpdateAsync(Basket basket)
    {
        await _context
            .Baskets
            .Where(d => d.Id == basket.Id)
            .ExecuteUpdateAsync(setters =>
                setters
                  .SetProperty(x => x.Id, basket.Id)
                  .SetProperty(x => x.CustomerId, basket.CustomerId)
                  .SetProperty(x => x.ItemIds, basket.ItemIds)
                  .SetProperty(x => x.AmountOfProducts, basket.AmountOfProducts)
                  .SetProperty(x => x.TotalAmount, basket.TotalAmount)
                  .SetProperty(x => x.CreatedOn, basket.CreatedOn)
                  .SetProperty(x => x.UpdatedOn, basket.UpdatedOn));
    }

    private async Task InsertBasket(Basket basket)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "INSERT INTO shopping.Baskets (BasketId," +
            "CustomerId, " +
            "AmountOfProducts, " +
            "TotalAmount, " +
            "CreatedOn, " +
            "UpdatedOn)" +
            "VALUES({0}, {1}, {2}, {3}, {4}, {5})",
            basket.Id.Value,
            basket.CustomerId,
            basket.AmountOfProducts,
            basket.TotalAmount,
            basket.CreatedOn,
            basket.UpdatedOn);
    }

    private async Task InsertBasketItems(Basket basket)
    {
        foreach (var itemId in basket.ItemIds)
        {
            await _context.Database.ExecuteSqlRawAsync(
            "INSERT INTO shopping.BasketItems (BasketId, ItemId) " +
            "VALUES ({0}, {1})",
            basket.Id.Value,
            itemId);
        }
    }
}
