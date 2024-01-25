using Shopping.Application.Common;
using ErrorOr;
using Shopping.Domain.Items;
using Shopping.Domain.Wishes;

namespace Shopping.Application.Wishes.AddItem;

internal sealed class AddItemToWishListCommandHandler : ICommandRequestHandler<AddItemToWishListCommand, ErrorOr<Guid>>
{
    private readonly IWishRepository _wishRepository;
    private readonly IItemRepository _itemRepository;

    public AddItemToWishListCommandHandler(IWishRepository wishRepository, IItemRepository itemRepository)
    {
        _wishRepository = wishRepository;
        _itemRepository = itemRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(AddItemToWishListCommand request, CancellationToken cancellationToken)
    {
        Wish? wish = await _wishRepository.GetByIdAsync(WishId.Create(request.WishId));

        if (wish is null)
        {
            return Error.NotFound("Wish.NotFound", "Wish list was not found");
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
