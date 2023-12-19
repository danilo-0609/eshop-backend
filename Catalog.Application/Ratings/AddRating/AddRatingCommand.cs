using BuildingBlocks.Application.Commands;
using ErrorOr;

namespace Catalog.Application.Ratings.AddRating;
public sealed record AddRatingCommand(
    Guid ProductId,
    double Rate,
    string Feedback = "") : ICommandRequest<ErrorOr<Guid>>;