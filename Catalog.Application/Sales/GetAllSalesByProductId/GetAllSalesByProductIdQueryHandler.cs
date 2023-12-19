using BuildingBlocks.Application.Queries;
using Catalog.Domain.Products;
using Catalog.Domain.Sales;
using ErrorOr;

namespace Catalog.Application.Sales.GetAllSalesByProductId;
internal sealed class GetAllSalesByProductIdQueryHandler : IQueryRequestHandler<GetAllSalesByProductIdQuery, ErrorOr<IReadOnlyList<SaleResponse>>>
{
    private readonly ISaleRepository _saleRepository;

    public GetAllSalesByProductIdQueryHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }
    public async Task<ErrorOr<IReadOnlyList<SaleResponse>>> Handle(GetAllSalesByProductIdQuery query, CancellationToken cancellationToken)
    {
        List<Sale>? sales = await _saleRepository.GetSalesByProductIdAsync(ProductId.Create(query.ProductId));
    
        if (sales is null)
        {
            return Error.NotFound("Sale.NotFound", "Sales in the product id provided were not found");
        }

        List<SaleResponse> response = new();

        sales.ForEach(sale => 
                    response.Add(item: new SaleResponse
                    (
                        Id: sale.Id.Value,
                        ProductId: sale.ProductId.Value,
                        AmountOfProducts: sale.AmountOfProducts,
                        Total: sale.UnitPrice,
                        UserId: sale.UserId,
                        OcurredOn: sale.CreatedDateTime
                    )));

        return response.AsReadOnly();
    }
}
