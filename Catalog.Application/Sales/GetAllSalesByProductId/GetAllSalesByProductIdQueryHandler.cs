using Catalog.Application.Common;
using Catalog.Domain.Products;
using Catalog.Domain.Sales;
using ErrorOr;

namespace Catalog.Application.Sales.GetAllSalesByProductId;
internal sealed class GetAllSalesByProductIdQueryHandler : IQueryRequestHandler<GetAllSalesByProductIdQuery, ErrorOr<IReadOnlyList<SaleResponse>>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly AuthorizationService _authorizationService;

    public GetAllSalesByProductIdQueryHandler(ISaleRepository saleRepository, AuthorizationService authorizationService)
    {
        _saleRepository = saleRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<IReadOnlyList<SaleResponse>>> Handle(GetAllSalesByProductIdQuery query, CancellationToken cancellationToken)
    {
        List<Sale>? sales = await _saleRepository.GetSalesByProductIdAsync(ProductId.Create(query.ProductId));
    
        if (sales is null)
        {
            return SalesErrorCodes.NotFound;
        }

        var authorizationService = _authorizationService.IsUserAuthorized(sales.First().UserId);

        if (authorizationService.IsError && _authorizationService.IsAdmin() is false)
        {
            return SalesErrorCodes.CannotAccessToContent;
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
