using Shopping.Application.Common;
using ErrorOr;

namespace Shopping.Application.Baskets.AddItem;

public sealed record AddItemToBasketCommand(
    Guid BasketId, 
    Guid ItemId) : ICommandRequest<ErrorOr<Guid>>;
