using BuildingBlocks.Domain;
using Shopping.Domain.Items;

namespace Shopping.Domain.Buying.Events;

public sealed record BuyGeneratedDomainEvent(
    Guid DomainEventId,
    BuyId BuyId,
    Guid BuyerId,
    ItemId ItemId,
    decimal UnitPrice,
    decimal TotalPrice,
    int AmountOfProducts,
    DateTime OcurredOn) : IDomainEvent;
