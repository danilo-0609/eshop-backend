using Shopping.Application.Common;
using ErrorOr;
using MediatR;

namespace Shopping.Application.Baskets.DeleteItem;

public sealed record DeleteItemFromBasketCommand(Guid BasketId, Guid ItemId) : ICommandRequest<ErrorOr<Unit>>;
