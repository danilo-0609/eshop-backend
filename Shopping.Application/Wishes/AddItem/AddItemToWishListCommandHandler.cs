using Shopping.Application.Common;
using ErrorOr;
using Shopping.Domain.Items;
using Shopping.Domain.Wishes;

namespace Shopping.Application.Wishes.AddItem;

internal sealed class AddItemToWishListCommandHandler : ICommandRequestHandler<AddItemToWishListCommand, ErrorOr<Guid>>
{
    private readonly IWishRepository _wishRepository;
    private readonly IItemRepository _itemRepository;
    private readonly AuthorizationService _authorizationService;

    public AddItemToWishListCommandHandler(IWishRepository wishRepository, IItemRepository itemRepository, AuthorizationService authorizationValidator)
    {
        _wishRepository = wishRepository;
        _itemRepository = itemRepository;
        _authorizationService = authorizationValidator;
    }

    public async Task<ErrorOr<Guid>> Handle(AddItemToWishListCommand request, CancellationToken cancellationToken)
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

        Item? item = await _itemRepository.GetByIdAsync(ItemId.Create(request.ItemId));

        if (item is null)
        {
            return Error.NotFound("Item.NotFound", "Item was not found");
        }

        wish.AddItem(item.Id);

        await _wishRepository.UpdateAsync(wish);

        return wish.Id.Value;
    }
}
