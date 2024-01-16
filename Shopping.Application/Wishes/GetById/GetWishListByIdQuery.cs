using BuildingBlocks.Application.Queries;
using ErrorOr;

namespace Shopping.Application.Wishes.GetById;

public sealed record GetWishListByIdQuery(
    Guid WishId) : IQueryRequest<ErrorOr<WishResponse>>;