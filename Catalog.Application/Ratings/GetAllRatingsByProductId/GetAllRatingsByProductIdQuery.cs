
using BuildingBlocks.Application.Commands;
using BuildingBlocks.Application.Queries;
using ErrorOr;

namespace Catalog.Application.Ratings.Catalog.Application.Ratings.GetAllRatingsByProductId;

public sealed record GetAllRatingsByProductIdQuery(Guid ProductId) 
    : IQueryRequest<ErrorOr<IReadOnlyList<RatingResponse>>>;