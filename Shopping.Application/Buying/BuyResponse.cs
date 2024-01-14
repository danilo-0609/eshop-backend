
namespace Shopping.Application.Buying;

public sealed record BuyResponse(
    Guid Id,
    Guid ItemId,
    int AmountOfProducts,
    decimal UnitPrice,
    decimal TotalAmount,
    DateTime OcurredOn);
