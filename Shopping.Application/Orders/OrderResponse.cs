namespace Shopping.Application.Orders;

public sealed record OrderResponse(
    Guid OrderId,
    Guid CustomerId,
    Guid ItemId,
    int AmountOfItems,
    decimal TotalMoneyAmount,
    string OrderStatus,
    DateTime PlacedOn);
