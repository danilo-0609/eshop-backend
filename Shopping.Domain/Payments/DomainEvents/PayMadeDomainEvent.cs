using BuildingBlocks.Domain;
using Shopping.Domain.Orders;

namespace Shopping.Domain.Payments.DomainEvents;

public sealed record PayMadeDomainEvent(
    Guid DomainEventId,
    PaymentId PaymentId,
    OrderId OrderId,
    Guid PayerId,
    decimal MoneyAmount,
    Guid ProductId,
    DateTime OcurredOn) : IDomainEvent;
