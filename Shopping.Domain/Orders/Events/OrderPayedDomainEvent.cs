using BuildingBlocks.Domain;
using Shopping.Domain.Items;

namespace Shopping.Domain.Orders.Events;

public sealed record OrderPayedDomainEvent(
    Guid DomainEventId,
    OrderId OrderId,
    Guid CustomerId,
    ItemId ItemId,
    int AmountOfProducts,
    int ActualStock,
    StockStatus StockStatus,
    decimal MoneyAmount,
    DateTime OcurredOn) : IDomainEvent;