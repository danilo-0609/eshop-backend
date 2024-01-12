﻿using BuildingBlocks.Domain;
using ErrorOr;
using Shopping.Domain.Orders.Errors;

namespace Shopping.Domain.Orders.Rules;

internal sealed class OrderCannotBeCompletedWhenOrderStatusIsNotPayedRule : IBusinessRule
{
    private readonly OrderStatus _orderStatus;

    public OrderCannotBeCompletedWhenOrderStatusIsNotPayedRule(OrderStatus orderStatus)
    {
        _orderStatus = orderStatus;
    }

    public Error Error => OrderErrors.OrderStatusIsNotPayed;

    public bool IsBroken() => _orderStatus != OrderStatus.Payed;

    public static string Message = "Order canot be completed when order status is not payed";
}
