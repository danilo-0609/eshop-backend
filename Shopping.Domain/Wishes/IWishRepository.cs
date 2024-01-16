namespace Shopping.Domain.Wishes;

public interface IWishRepository
{
    Task AddAsync(Wish wish);

    Task UpdateAsync(Wish wish);

    Task DeleteAsync(Wish wish);

    Task<Wish?> GetByIdAsync(WishId wishId);
}
