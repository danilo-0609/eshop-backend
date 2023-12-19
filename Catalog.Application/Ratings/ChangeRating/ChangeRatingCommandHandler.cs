using BuildingBlocks.Application;
using BuildingBlocks.Application.Commands;
using Catalog.Domain.Ratings;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Ratings.ChangeRating;
internal sealed class ChangeRatingCommandHandler : ICommandRequestHandler<ChangeRatingCommand, ErrorOr<Unit>>
{
    private readonly IRatingRepository _ratingRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public ChangeRatingCommandHandler(IRatingRepository ratingRepository, IExecutionContextAccessor executionContextAccessor)
    {
        _ratingRepository = ratingRepository;
        _executionContextAccessor = executionContextAccessor;

    }

    public async Task<ErrorOr<Unit>> Handle(ChangeRatingCommand command, CancellationToken cancellationToken)
    {
        Rating? rating = await _ratingRepository.GetByIdAsync(RatingId.Create(command.RatingId));

        if (rating is null)
        {
            return Error.NotFound("Rating.NotFound", "Rating was not found");
        }

        if (_executionContextAccessor.UserId != rating.UserId)
        {
            return Error.Unauthorized("User.NotAuthorized", "Cannot update if you are not authorized");
        }

        var update = Rating.Update(rating.Id,
            rating.UserId,
            command.Rate,
            rating.ProductId,
            rating.CreatedDateTime,
            DateTime.UtcNow,
            command.Feedback);

        await _ratingRepository.UpdateAsync(update);

        return Unit.Value;
    }

}
