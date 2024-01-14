using BuildingBlocks.Application.Commands;
using ErrorOr;

namespace Shopping.Application.Baskets.CreateBasket;

public sealed record CreateBasketCommand(Guid ItemId) : ICommandRequest<ErrorOr<Guid>>;