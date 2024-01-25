using ErrorOr;
using Shopping.Application.Common;

namespace Shopping.Application.Baskets.CreateBasket;

public sealed record CreateBasketCommand(Guid ItemId) : ICommandRequest<ErrorOr<Guid>>;