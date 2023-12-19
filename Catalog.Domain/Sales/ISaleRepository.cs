using Catalog.Domain.Products;

namespace Catalog.Domain.Sales;
public interface ISaleRepository
{
    Task AddAsync(Sale sale);

    Task<List<Sale>?> GetSalesByProductIdAsync(ProductId productId);

    Task<Sale?> GetByIdAsync(SaleId saleId);       
}
