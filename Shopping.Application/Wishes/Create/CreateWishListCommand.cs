using BuildingBlocks.Application.Commands;
using ErrorOr;

namespace Shopping.Application.Wishes.Create;

public sealed record CreateWishListCommand(
    Guid ItemId,
    string Name,
    bool IsPrivate) : ICommandRequest<ErrorOr<Guid>>;
