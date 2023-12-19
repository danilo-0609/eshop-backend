using Catalog.Domain.Products;

namespace Catalog.Domain.Ratings;

public interface IRatingRepository
{
    Task AddAsync(Rating rating);

    Task UpdateAsync(Rating rating);

    Task DeleteAsync(Rating rating);

    Task<Rating?> GetByIdAsync(RatingId ratingId);

    Task<List<Rating>> GetRatingsByProductIdAsync(ProductId productId);         
}
