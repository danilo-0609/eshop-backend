using BuildingBlocks.Domain;

namespace Catalog.Domain.Products.Events;

public sealed record ProductPublishedDomainEvent(Guid DomainEventId,
    ProductId ProductId,
    Guid SellerId,
    string Name,
    string Description,
    decimal Price,
    int InStock,
    DateTime OcurredOn) : IDomainEvent;

