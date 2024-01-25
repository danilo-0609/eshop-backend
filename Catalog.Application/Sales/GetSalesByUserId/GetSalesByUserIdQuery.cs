using Catalog.Application.Common;
using ErrorOr;

namespace Catalog.Application.Sales.GetAllSalesByUserId;

public sealed record GetSalesByUserIdQuery(Guid UserId) 
    : IQueryRequest<ErrorOr<IReadOnlyList<SaleResponse>>>;
    