using Shopping.Application.Common;
using ErrorOr;
using MediatR;
using Shopping.Domain.Items;
using Shopping.Domain.Wishes;

namespace Shopping.Application.Wishes.RemoveItem;

internal sealed class RemoveItemFromWishListCommandHandler : ICommandRequestHandler<RemoveItemFromWishListCommand, ErrorOr<Unit>>
{
    private readonly IWishRepository _wishRepository;

    public RemoveItemFromWishListCommandHandler(IWishRepository wishRepository)
    {
        _wishRepository = wishRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(RemoveItemFromWishListCommand request, CancellationToken cancellationToken)
    {
        var wish = await _wishRepository.GetByIdAsync(WishId.Create(request.WishId));

        if (wish is null)
        {
            return Error.NotFound("Wish.NotFound", "Wish list was not found");
        }

        wish.RemoveItem(ItemId.Create(request.ItemId));

        await _wishRepository.UpdateAsync(wish);

        return Unit.Value;
    }
}
