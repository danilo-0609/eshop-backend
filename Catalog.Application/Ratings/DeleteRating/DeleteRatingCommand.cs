using Catalog.Application.Common;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Ratings.DeleteRating;

public sealed record DeleteRatingCommand(Guid Id) : ICommandRequest<ErrorOr<Unit>>; 