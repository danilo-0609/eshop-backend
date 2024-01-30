using Shopping.Application.Common;
using ErrorOr;
using MediatR;
using Shopping.Domain.Wishes;

namespace Shopping.Application.Wishes.Delete;

internal sealed class DeleteWishListCommandHandler : ICommandRequestHandler<DeleteWishListCommand, ErrorOr<Unit>>
{
    private readonly IWishRepository _wishRepository;
    private readonly IAuthorizationService _authorizationService;

    public DeleteWishListCommandHandler(IWishRepository wishRepository, IAuthorizationService authorizationService)
    {
        _wishRepository = wishRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteWishListCommand request, CancellationToken cancellationToken)
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

        await _wishRepository.DeleteAsync(wish);

        return Unit.Value;
    }
}
