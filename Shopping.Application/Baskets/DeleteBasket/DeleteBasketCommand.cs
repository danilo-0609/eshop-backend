using ErrorOr;
using Shopping.Application.Common;

namespace Shopping.Application.Baskets.DeleteBasket;

public sealed record DeleteBasketCommand(Guid BasketId) : ICommandRequest<ErrorOr<Guid>>;