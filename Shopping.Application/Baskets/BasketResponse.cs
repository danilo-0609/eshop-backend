namespace Shopping.Application.Baskets;

public sealed record BasketResponse(
    Guid BasketId,
    Guid CustomerId,
    List<Guid> ItemIds,
    int AmountOfProducts,
    decimal TotalAmount,
    DateTime CreatedOn);