using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        List<Item> itemIds = await _context.Items
        .FromSqlRaw(
        @"SELECT    
            i.ItemId,
            i.Name,
            i.SellerId,
            i.Price,
            i.InStock,
            i.StockStatus,
            i.CreatedOn,
            i.UpdatedOn
          FROM shopping.Items i 
          INNER JOIN shopping.BasketItems bi ON i.ItemId = bi.ItemId
          INNER JOIN shopping.Baskets b ON bi.BasketId = b.BasketId
          WHERE b.BasketId = {0}",
        basketId)
       .ToListAsync();


        return itemIds.Select(r => r.Id.Value).ToList();
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