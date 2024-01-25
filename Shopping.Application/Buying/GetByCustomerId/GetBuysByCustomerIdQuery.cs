using Shopping.Application.Common;
using ErrorOr;

namespace Shopping.Application.Buying.GetByCustomerId;

public sealed record GetBuysByCustomerIdQuery(Guid CustomerId) : IQueryRequest<ErrorOr<IReadOnlyList<BuyResponse>>>;
