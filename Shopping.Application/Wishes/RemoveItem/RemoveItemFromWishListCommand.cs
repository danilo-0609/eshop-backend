using Shopping.Application.Common;
using ErrorOr;
using MediatR;

namespace Shopping.Application.Wishes.RemoveItem;

public sealed record RemoveItemFromWishListCommand(
    Guid WishId,
    Guid ItemId) : ICommandRequest<ErrorOr<Unit>>;
