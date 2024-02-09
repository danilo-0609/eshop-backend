namespace Shopping.Domain.Basket;

public interface IBasketRepository
{
    Task AddAsync(Basket basket);

    Task<Basket?> GetByIdAsync(BasketId basketId);

    Task UpdateAsync(Basket basket);

    Task DeleteAsync(Basket basket);

    Task<Dictionary<Guid, int>> GetBasketItemIdsAsync(Guid basketId);

    Task DeleteItemInBasketIdAsync(Guid itemId, Guid BasketId, int amountOfProducts, decimal moneyAmount);
}
