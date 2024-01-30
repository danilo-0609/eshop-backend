using Shopping.Application.Common;
using ErrorOr;
using MediatR;
using Shopping.Domain.Wishes;

namespace Shopping.Application.Wishes.ChangeVisibility;

internal sealed class ChangeWishVisibilityCommandHandler : ICommandRequestHandler<ChangeWishVisibilityCommand, ErrorOr<Unit>>
{
    private readonly IWishRepository _wishRepository;
    private readonly AuthorizationService _authorizationService;

    public ChangeWishVisibilityCommandHandler(IWishRepository wishRepository, AuthorizationService authorizationService)
    {
        _wishRepository = wishRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Unit>> Handle(ChangeWishVisibilityCommand request, CancellationToken cancellationToken)
    {
        Wish? wish = await _wishRepository.GetByIdAsync(WishId.Create(request.WishId));

        if (wish is null)
        {
            return WishErrorCodes.NotFound;
        }

        var authorizationService = _authorizationService.IsUserAuthorized(wish.CustomerId);

        if (authorizationService.IsError)
        {
            return WishErrorCodes.UserNotAuthorizedToAccess;
        }
        
        wish.ChangeVisibility(request.Visibility);

        await _wishRepository.UpdateAsync(wish);

        return Unit.Value;
    }
}
