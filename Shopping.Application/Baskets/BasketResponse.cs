namespace Shopping.Application.Baskets;

public sealed record BasketResponse(
    Guid BasketId,
    Guid CustomerId,
    IReadOnlyDictionary<Guid, int> ItemIds,
    int AmountOfProducts,
    decimal TotalAmount,
    DateTime CreatedOn);