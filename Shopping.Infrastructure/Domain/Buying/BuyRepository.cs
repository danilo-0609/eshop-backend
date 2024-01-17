using Microsoft.EntityFrameworkCore;
using Shopping.Domain.Buying;

namespace Shopping.Infrastructure.Domain.Buying;

internal sealed class BuyRepository : IBuyRepository
{
    private readonly ShoppingDbContext _dbContext;

    public BuyRepository(ShoppingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Buy buy)
    {
        await _dbContext
            .Buys
            .AddAsync(buy);
    }

    public async Task<List<Buy>?> GetBuysByCustomerId(Guid customerId)
    {
        var buys = await _dbContext
            .Buys
            .Where(x => x.BuyerId == customerId)
            .ToListAsync();

        return buys;
    }
}
