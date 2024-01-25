using Catalog.Application.Common;
using ErrorOr;

namespace Catalog.Application.Sales.GetAllSalesByProductId;

public sealed record GetAllSalesByProductIdQuery(Guid ProductId) 
    : IQueryRequest<ErrorOr<IReadOnlyList<SaleResponse>>>;
