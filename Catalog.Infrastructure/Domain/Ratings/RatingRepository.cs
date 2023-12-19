using Catalog.Domain.Products;
using Catalog.Domain.Ratings;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Domain.Ratings;

internal sealed class RatingRepository : IRatingRepository
{
    private readonly CatalogDbContext _dbContext;

    public RatingRepository(CatalogDbContext applicationDbContext)
    {
        _dbContext = applicationDbContext;
    }

    public async Task AddAsync(Rating rating)
    {
        await _dbContext
        .Ratings
        .AddAsync(rating);
    }

    public async Task DeleteAsync(Rating rating)
    {
        await  _dbContext
            .Ratings
            .Where(d => d.Id == rating.Id)
            .ExecuteDeleteAsync();
    }

    public async Task<Rating?> GetByIdAsync(RatingId ratingId)
    {
        return await _dbContext
        .Ratings
        .Where(d => d.Id == ratingId)
        .SingleOrDefaultAsync();
    }

    public async Task<List<Rating>> GetRatingsByProductIdAsync(ProductId productId)
    {
        List<Rating> ratings = await _dbContext
            .Ratings
            .Where(x => x.ProductId == productId)
            .ToListAsync();

        return ratings;
    }

    public async Task UpdateAsync(Rating rating)
    {
        await _dbContext
            .Ratings
            .Where(d => d.Id == rating.Id)
            .ExecuteUpdateAsync(setters => setters 
                .SetProperty(s => s.Id, rating.Id)
                .SetProperty(s => s.Feedback, rating.Feedback)
                .SetProperty(s => s.UserId, rating.UserId)
                .SetProperty(s => s.Rate, rating.Rate)
                .SetProperty(s => s.CreatedDateTime, rating.CreatedDateTime)
                .SetProperty(s => s.UpdatedDateTime, rating.UpdatedDateTime));
    }
}