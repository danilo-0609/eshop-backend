using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;

namespace Shopping.Application.Wishes.Delete;

public sealed record DeleteWishListCommand(
    Guid WishId) : ICommandRequest<ErrorOr<Unit>>;
