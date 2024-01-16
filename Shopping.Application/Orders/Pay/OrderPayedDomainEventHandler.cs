﻿using BuildingBlocks.Application.Events;
using MediatR;
using Shopping.Application.Common;
using Shopping.Domain.Orders.Events;
using Shopping.IntegrationEvents;

namespace Shopping.Application.Orders.Pay;

internal sealed class OrderPayedDomainEventHandler : IDomainEventHandler<OrderPayedDomainEvent>
{
    private readonly ISender _sender;
    private readonly IShoppingEventBus _shoppingEventBus;

    public OrderPayedDomainEventHandler(ISender sender, IShoppingEventBus shoppingEventBus)
    {
        _sender = sender;
        _shoppingEventBus = shoppingEventBus;
    }

    public async Task Handle(OrderPayedDomainEvent notification, CancellationToken cancellationToken)
    {
        await _shoppingEventBus.PublishAsync(new OrderPayedIntegrationEvent(
            notification.DomainEventId,
            notification.ItemId.Value,
            notification.OrderId.Value,
            notification.AmountOfProducts,
            DateTime.UtcNow));
    }
}