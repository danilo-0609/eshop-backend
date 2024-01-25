using ErrorOr;
using Shopping.Application.Common;

namespace Shopping.Application.Baskets.BuyBasket;

public sealed record BuyBasketCommand(Guid BasketId) : ICommandRequest<ErrorOr<Guid>>;
