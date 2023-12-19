using BuildingBlocks.Domain;

namespace Catalog.Domain.Products.Events;
public record ProductRemovedDomainEvent(Guid DomainEventId,
    ProductId ProductId,
    DateTime OcurredOn) : IDomainEvent;
