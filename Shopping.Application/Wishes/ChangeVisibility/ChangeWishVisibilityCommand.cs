using Shopping.Application.Common;
using ErrorOr;
using MediatR;

namespace Shopping.Application.Wishes.ChangeVisibility;

public sealed record ChangeWishVisibilityCommand(
    Guid WishId,
    bool Visibility) : ICommandRequest<ErrorOr<Unit>>;
