using Shopping.Application.Common;
using ErrorOr;

namespace Shopping.Application.Baskets.AddItem;

public sealed record AddItemToBasketCommand(
    Guid BasketId, 
    Guid ItemId,
    int Amount) : ICommandRequest<ErrorOr<Guid>>;
