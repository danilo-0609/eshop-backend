using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Shopping.Domain.Basket;
using Shopping.Domain.Items;

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
        await DeleteBasketItemIdsAsync(basket.Id.Value);
        await DeleteBasket(basket.Id.Value);
    }

    public async Task<Basket?> GetByIdAsync(BasketId basketId)
    {
        return await _context
            .Baskets
            .Where(d => d.Id == basketId)
            .SingleOrDefaultAsync();
    }

    public async Task<Dictionary<Guid, int>> GetBasketItemIdsAsync(Guid basketId)
    {
        Dictionary<Guid, int> amountsPerItem = await _context
            .BasketItems
            .FromSqlRaw(
            @"SELECT TOP (100) PERCENT
                bi.ItemId,
                bi.AmountPerItem
            FROM shopping.BasketItems bi
            WHERE BasketId = @BasketId",
            new SqlParameter("@BasketId", basketId))
            .Select(r => new { r.ItemId, r.AmountPerItem })
            .ToDictionaryAsync(i => i.ItemId, o => o.AmountPerItem);

        return amountsPerItem;
    }

    private async Task DeleteBasket(Guid basketId)
    {
        await _context
            .Database
            .ExecuteSqlRawAsync(
            @"DELETE FROM shopping.Baskets 
              WHERE BasketId = @BasketId",
            new SqlParameter("@BasketId", basketId));
    }

    public async Task DeleteBasketItemIdsAsync(Guid basketId)
    {
        await _context
            .Database
            .ExecuteSqlRawAsync(
            @"DELETE FROM shopping.BasketItems
            WHERE BasketId = @BasketId",
            new SqlParameter("@BasketId", basketId));
    }

    public async Task DeleteItemInBasketIdAsync(Guid itemId, Guid basketId, int amount, decimal moneyAmount)
    {
        await _context
        .Database
        .ExecuteSqlRawAsync(
        @"DELETE FROM shopping.BasketItems
          WHERE ItemId = @ItemId AND 
                BasketId = @BasketId",
        new SqlParameter("@ItemId", itemId),
        new SqlParameter("@BasketId", basketId));

        await _context
            .Database
            .ExecuteSqlRawAsync(
            @"UPDATE shopping.Baskets
            SET AmountOfProducts = @AmountOfProducts,
            TotalAmount = @TotalAmount",
            new SqlParameter("@AmountOfProducts", amount),
            new SqlParameter("@TotalAmount", moneyAmount));
    }

    public async Task UpdateAsync(Basket basket)
    {
        SqlParameter updatedOn = basket.UpdatedOn != null ?
            new SqlParameter("@UpdatedOn", basket.UpdatedOn) :
            new SqlParameter("@UpdatedOn", DBNull.Value);

        await _context
            .Database
            .ExecuteSqlRawAsync(
            """
            UPDATE shopping.Baskets
            SET AmountOfProducts = @AmountOfProducts,
            TotalAmount = @TotalAmount,
            UpdatedOn = @UpdatedOn
            WHERE BasketId = @BasketId;
            """,
            new SqlParameter("@AmountOfProducts", basket.AmountOfProducts),
            new SqlParameter("@TotalAmount", basket.TotalAmount),
            updatedOn,
            new SqlParameter("@BasketId", basket.Id.Value));

        await InsertBasketItems(basket);
    }

    private async Task InsertBasket(Basket basket)
    {
        SqlParameter updatedOn = basket.UpdatedOn != null ?
            new SqlParameter("@UpdatedOn", basket.UpdatedOn) :
            new SqlParameter("@UpdatedOn", DBNull.Value);

        await _context
            .Database
            .ExecuteSqlRawAsync(
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
        foreach (ItemId itemId in basket.ItemIds)
        {
            if (_context
                .BasketItems
                .Any(r => r.ItemId == itemId.Value && r.BasketId == basket.Id.Value))
            {
                await _context.Database.ExecuteSqlRawAsync(
                @"UPDATE shopping.BasketItems 
                  SET AmountPerItem = @AmountPerItem 
                  WHERE ItemId = @ItemId",
                new SqlParameter("@AmountPerItem", basket.AmountPerItem[itemId.Value]),
                new SqlParameter("@ItemId", itemId.Value));
                    
                continue;
            }

            await _context.Database.ExecuteSqlRawAsync(
            @"INSERT INTO shopping.BasketItems (BasketId, ItemId, AmountPerItem) " +
            "VALUES (@BasketId, @ItemId, @AmountPerItem)",
            new SqlParameter("@BasketId", basket.Id.Value),
            new SqlParameter("@ItemId", itemId.Value),
            new SqlParameter("@AmountPerItem", basket.AmountPerItem[itemId.Value]));
        }
    }
}