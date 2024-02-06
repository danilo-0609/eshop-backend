using MassTransit.NewIdProviders;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shopping.Domain.Basket;
using Shopping.Domain.Items;
using Shopping.Domain.Orders;

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

    public async Task<List<Guid>> GetBasketItemIdsAsync(Guid basketId)
    {
        List<Item> items = await _context
            .Items
            .FromSqlRaw(
            @"SELECT TOP (100) PERCENT 
	            i.ItemId,
            FROM shopping.Items i 
            INNER JOIN shopping.BasketItems bi ON i.ItemId = bi.ItemId
            INNER JOIN shopping.Baskets b ON bi.BasketId = b.BasketId
            WHERE b.BasketId = @BasketId;", 
            new SqlParameter("@BasketId", basketId))
            .ToListAsync();

        var itemIds = new List<Guid>();

        foreach (var item in items)
        {
            itemIds.Add(item.Id.Value);
        }

        return itemIds;
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
        SqlParameter updatedOn = basket.UpdatedOn != null ?
            new SqlParameter("@UpdatedOn", basket.UpdatedOn) :
            new SqlParameter("@UpdatedOn", DBNull.Value);

        await _context.Database.ExecuteSqlRawAsync(
            @"INSERT INTO shopping.Baskets (BasketId, " +
            "CustomerId, " +
            "AmountOfProducts, " +
            "TotalAmount, " +
            "CreatedOn, " +
            "UpdatedOn)" +
            "VALUES(@Id, @CustomerId, @AmountOfProducts, @TotalAmount, @CreatedOn, @UpdatedOn)",
            new SqlParameter("@Id", basket.Id.Value),
            new SqlParameter("@CustomerId", basket.CustomerId),
            new SqlParameter("@AmountOfProducts", basket.AmountOfProducts),
            new SqlParameter("@TotalAmount", basket.TotalAmount),
            new SqlParameter("@CreatedOn", basket.CreatedOn),
            updatedOn);
    }

    private async Task InsertBasketItems(Basket basket)
    {
        foreach (var itemId in basket.ItemIds)
        {
            await _context.Database.ExecuteSqlRawAsync(
            @"INSERT INTO shopping.BasketItems (BasketId, ItemId) " +
            "VALUES (@BasketId, @ItemId)",
            new SqlParameter("@BasketId", basket.Id.Value),
            new SqlParameter("@ItemId", itemId.Value));
        }
    }
}
