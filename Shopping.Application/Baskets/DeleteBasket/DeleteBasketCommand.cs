using BuildingBlocks.Application.Commands;
using ErrorOr;

namespace Shopping.Application.Baskets.DeleteBasket;

public sealed record DeleteBasketCommand(Guid BasketId) : ICommandRequest<ErrorOr<Guid>>;