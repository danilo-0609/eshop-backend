namespace Shopping.Domain.Basket;

public interface IBasketRepository
{
    Task AddAsync(Basket basket);
}
