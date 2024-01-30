using Shopping.Application.Common;
using ErrorOr;

namespace Shopping.Application.Baskets.GetBasketById;

public sealed record GetBasketByIdQuery(Guid BasketId) : IQueryRequest<ErrorOr<BasketResponse>>;
