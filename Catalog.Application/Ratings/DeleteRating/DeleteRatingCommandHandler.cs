using BuildingBlocks.Application;
using Catalog.Application.Common;
using Catalog.Domain.Ratings;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Ratings.DeleteRating;

internal sealed class DeleteRatingCommandHandler : ICommandRequestHandler<DeleteRatingCommand, ErrorOr<Unit>>
{
    private readonly IRatingRepository _ratingRepository;
    private readonly AuthorizationService _authorizationService;

    public DeleteRatingCommandHandler(IRatingRepository ratingRepository, AuthorizationService authorizationService)
    {
        _ratingRepository = ratingRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteRatingCommand command, CancellationToken cancellationToken)
    {
        Rating? rating = await _ratingRepository.GetByIdAsync(RatingId.Create(command.Id));

        if (rating is null)
        {
            return RatingErrorCodes.NotFound;
        }

        var authorizationService = _authorizationService.IsUserAuthorized(rating.UserId);

        if (authorizationService.IsError)
        {
            return RatingErrorCodes.CannotAccessToContent;
        }
        
        await _ratingRepository.DeleteAsync(rating);

        return Unit.Value;
    }
}