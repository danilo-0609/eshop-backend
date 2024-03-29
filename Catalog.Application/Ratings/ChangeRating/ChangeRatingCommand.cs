using Catalog.Application.Common;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Ratings.ChangeRating;
public sealed record ChangeRatingCommand(
    Guid RatingId,
    double Rate,
    string Feedback = "") : ICommandRequest<ErrorOr<Unit>>;
