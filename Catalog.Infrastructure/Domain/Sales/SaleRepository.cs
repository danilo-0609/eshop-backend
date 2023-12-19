using Catalog.Domain.Products;
using Catalog.Domain.Sales;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Domain.Sales;
internal sealed class SaleRepository : ISaleRepository
{
    private readonly CatalogDbContext _dbContext;

    public SaleRepository(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Sale sale)
    {
        await _dbContext
            .Sales
            .AddAsync(sale);
    }

    public async Task<Sale?> GetByIdAsync(SaleId saleId)
    {
        return await _dbContext.Sales.Where(d => d.Id == saleId).SingleOrDefaultAsync();
    }

    public async Task<List<Sale>?> GetSalesByProductIdAsync(ProductId productId)
    {
        List<Sale> sales = await _dbContext
            .Sales
            .Where(p => p.ProductId == productId)
            .ToListAsync();

        if (sales.Count == 0)
        {
            return null;
        }

        return sales;
    }
}