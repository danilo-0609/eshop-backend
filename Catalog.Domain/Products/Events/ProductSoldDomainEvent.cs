using System;
using BuildingBlocks.Domain;

namespace Catalog.Domain.Products.Events;
public record ProductSoldDomainEvent(Guid DomainEventId,
    ProductId ProductId,
    int Amount,
    decimal UnitPrice,
    Guid UserId,
    Guid OrderId,
    DateTime OcurredOn) : IDomainEvent;
