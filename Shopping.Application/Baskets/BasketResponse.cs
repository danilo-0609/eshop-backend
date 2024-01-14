namespace Shopping.Application.Baskets;

public sealed record BasketResponse(
    Guid BasketId,
    Guid CustomerId,
    IReadOnlyList<Guid> ItemIds,
    int AmountOfProducts,
    decimal TotalAmount,
    DateTime CreatedOn);