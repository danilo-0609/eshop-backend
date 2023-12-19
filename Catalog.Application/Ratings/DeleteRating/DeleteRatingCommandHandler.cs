using BuildingBlocks.Application;
using BuildingBlocks.Application.Commands;
using Catalog.Domain.Ratings;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Ratings.DeleteRating;

internal sealed class DeleteRatingCommandHandler : ICommandRequestHandler<DeleteRatingCommand, ErrorOr<Unit>>
{
    private readonly IRatingRepository _ratingRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public DeleteRatingCommandHandler(IRatingRepository ratingRepository, IExecutionContextAccessor executionContextAccessor)
    {
        _ratingRepository = ratingRepository;
        _executionContextAccessor = executionContextAccessor;

    }

    public async Task<ErrorOr<Unit>> Handle(DeleteRatingCommand command, CancellationToken cancellationToken)
    {
        Rating? rating = await _ratingRepository.GetByIdAsync(RatingId.Create(command.Id));

        if (rating is null)
        {
            return Error.NotFound("Rating.NotFound", "Rating was not found");
        }

        if (_executionContextAccessor.UserId != rating.UserId)
        {
            return Error.Unauthorized("User.NotAuthorized", "Cannot delete if you are not authorized");
        }

        await _ratingRepository.DeleteAsync(rating);

        return Unit.Value;
    }
}