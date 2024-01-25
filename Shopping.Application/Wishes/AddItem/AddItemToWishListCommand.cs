using Shopping.Application.Common;
using ErrorOr;

namespace Shopping.Application.Wishes.AddItem;

public sealed record AddItemToWishListCommand(
    Guid WishId,
    Guid ItemId) : ICommandRequest<ErrorOr<Guid>>;
