using Catalog.Application.Common;
using Catalog.Domain.Ratings;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Ratings.ChangeRating;

internal sealed class ChangeRatingCommandHandler : ICommandRequestHandler<ChangeRatingCommand, ErrorOr<Unit>>
{
    private readonly IRatingRepository _ratingRepository;
    private readonly IAuthorizationService _authorizationService;

    public ChangeRatingCommandHandler(IRatingRepository ratingRepository, IAuthorizationService authorizationService)
    {
        _ratingRepository = ratingRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Unit>> Handle(ChangeRatingCommand command, CancellationToken cancellationToken)
    {
        Rating? rating = await _ratingRepository.GetByIdAsync(RatingId.Create(command.RatingId));

        if (rating is null)
        {
            return RatingErrorCodes.NotFound;
        }

        var authorizationService = _authorizationService.IsUserAuthorized(rating.UserId);

        if (authorizationService.IsError)
        {
            return RatingErrorCodes.CannotAccessToContent;
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
