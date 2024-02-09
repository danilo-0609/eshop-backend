using BuildingBlocks.Domain;

namespace Shopping.Domain.Basket.Events;

public sealed record BasketBuyRequestedDomainEvent(
    Guid DomainEventId,
    BasketId BasketId,
    Guid CustomerId,
    IReadOnlyDictionary<Guid, int> ItemIds,
    decimal TotalAmount,
    DateTime OcurredOn) : IDomainEvent;
