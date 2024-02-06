namespace Shopping.Domain.Basket;

public interface IBasketRepository
{
    Task AddAsync(Basket basket);

    Task<Basket?> GetByIdAsync(BasketId basketId);

    Task UpdateAsync(Basket basket);

    Task DeleteAsync(Basket basket);

    Task<List<Guid>> GetBasketItemIdsAsync(Guid basketId);
}
