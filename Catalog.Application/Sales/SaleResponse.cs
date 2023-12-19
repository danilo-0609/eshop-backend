namespace Catalog.Application.Sales;
public sealed record SaleResponse(
    Guid Id,
    Guid ProductId,
    int AmountOfProducts,
    decimal Total,
    Guid UserId,
    DateTime OcurredOn);