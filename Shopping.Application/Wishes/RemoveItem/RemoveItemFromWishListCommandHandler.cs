using Shopping.Application.Common;
using ErrorOr;
using MediatR;
using Shopping.Domain.Items;
using Shopping.Domain.Wishes;

namespace Shopping.Application.Wishes.RemoveItem;

internal sealed class RemoveItemFromWishListCommandHandler : ICommandRequestHandler<RemoveItemFromWishListCommand, ErrorOr<Unit>>
{
    private readonly IWishRepository _wishRepository;
    private readonly IAuthorizationService _authorizationService;

    public RemoveItemFromWishListCommandHandler(IWishRepository wishRepository, IAuthorizationService authorizationService)
    {
        _wishRepository = wishRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Unit>> Handle(RemoveItemFromWishListCommand request, CancellationToken cancellationToken)
    {
        var wish = await _wishRepository.GetByIdAsync(WishId.Create(request.WishId));

        if (wish is null)
        {
            return WishErrorCodes.NotFound;
        }

        var authorizationService = _authorizationService.IsUserAuthorized(wish.CustomerId);

        if (authorizationService.IsError)
        {
            return WishErrorCodes.UserNotAuthorizedToAccess;
        }

        wish.RemoveItem(ItemId.Create(request.ItemId));

        await _wishRepository.UpdateAsync(wish);

        return Unit.Value;
    }
}
