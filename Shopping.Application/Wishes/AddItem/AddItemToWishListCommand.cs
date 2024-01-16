using BuildingBlocks.Application.Commands;
using ErrorOr;

namespace Shopping.Application.Wishes.AddItem;

public sealed record AddItemToWishListCommand(
    Guid WishId,
    Guid ItemId) : ICommandRequest<ErrorOr<Guid>>;
