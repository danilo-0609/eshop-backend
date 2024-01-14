using BuildingBlocks.Application.Commands;
using ErrorOr;

namespace Shopping.Application.Baskets.BuyBasket;

public sealed record BuyBasketCommand(Guid BasketId) : ICommandRequest<ErrorOr<Guid>>;
