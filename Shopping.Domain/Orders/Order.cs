using BuildingBlocks.Domain;
using ErrorOr;
using MediatR;
using Shopping.Domain.Items;
using Shopping.Domain.Orders.Events;
using Shopping.Domain.Orders.Rules;
using Shopping.Domain.Payments;

namespace Shopping.Domain.Orders;

public sealed class Order : AggregateRoot<OrderId, Guid>
{
    public new OrderId Id { get; private set; }

    public Guid CustomerId { get; private set; }    

    public ItemId ItemId { get; private set; }

    public int AmountOfItems { get; private set; }

    public decimal TotalMoneyAmount { get; private set; }

    public OrderStatus OrderStatus { get; private set; }

    public DateTime PlacedOn { get; private set; }

    public DateTime? ConfirmedOn { get; private set; }

    public DateTime? ExpiredOn { get; private set; }

    public DateTime? PayedOn { get; private set; }

    public DateTime? CompletedOn { get; private set; }

    private Order(
        OrderId id,
        Guid customerId,
        ItemId itemId,
        int amountOfItems,
        decimal totalMoneyAmount,
        OrderStatus orderStatus,
        DateTime placedOn,
        DateTime? confirmedOn = null,
        DateTime? expiredOn = null,
        DateTime? payedOn = null,
        DateTime? completedOn = null)
    {
        Id = id;
        CustomerId = customerId;
        ItemId = itemId;
        AmountOfItems = amountOfItems;
        TotalMoneyAmount = totalMoneyAmount;
        OrderStatus = orderStatus;

        PlacedOn = placedOn;
        ConfirmedOn = confirmedOn;
        ExpiredOn = expiredOn;
        PayedOn = payedOn;
        CompletedOn = completedOn;
    }

    public static ErrorOr<Order> Place(
        ItemId itemId,
        Guid customerId,
        DateTime placedOn,
        int amountRequested,
        int actualStock,
        decimal totalMoneyAmount,
        StockStatus stockStatus)
    {
        Order order = new Order(
            OrderId.CreateUnique(),
            customerId,
            itemId,
            amountRequested,
            totalMoneyAmount,
            OrderStatus.Placed,
            DateTime.UtcNow);

        var cannotBePlacedIfItemIsOutOfStock = order.CheckRule(new OrderCannotBePlacedWhenItemIsOutOfStockRule(stockStatus));

        if (cannotBePlacedIfItemIsOutOfStock.IsError)
        {
            return cannotBePlacedIfItemIsOutOfStock.FirstError;
        }

        var amountCannotBeGreaterThanActualAmount = order.CheckRule(new OrderItemAmountCannotBeGreaterThanItemActualAmountRule(amountRequested, actualStock));

        if (amountCannotBeGreaterThanActualAmount.IsError)
        {
            return amountCannotBeGreaterThanActualAmount.FirstError;
        }

        order.PlacedOn = placedOn;
        order.OrderStatus = OrderStatus.Placed;

        order.Raise(new OrderPlacedDomainEvent(
            Guid.NewGuid(),
            order.Id,
            order.CustomerId,
            order.ItemId,
            order.OrderStatus,
            order.AmountOfItems,
            order.TotalMoneyAmount,
            order.PlacedOn));

        return order;
    }

    public ErrorOr<Unit> Confirm(DateTime confirmedOn)
    {
        var cannotBeConfirmedIfOrderStatusIsConfirmed = CheckRule(new OrderCannotBeConfirmedWhenOrderStatusIsConfirmedRule(OrderStatus));

        if (cannotBeConfirmedIfOrderStatusIsConfirmed.IsError)
        {
            return cannotBeConfirmedIfOrderStatusIsConfirmed.FirstError;
        }

        var cannotBeConfirmedAfterExpiration = CheckRule(new OrderCannotBeConfirmedAfterExpirationRule(OrderStatus)); ;

        if (cannotBeConfirmedAfterExpiration.IsError)
        {
            return cannotBeConfirmedAfterExpiration.FirstError;
        }

        var cannotBeConfirmedWhenOrderStatusIsPayed = CheckRule(new OrderCannotBeConfirmedWhenOrderStatusIsPayedRule(OrderStatus));

        if (cannotBeConfirmedWhenOrderStatusIsPayed.IsError)
        {
            return cannotBeConfirmedWhenOrderStatusIsPayed.FirstError;
        }

        var cannotBeConfirmedWhenOrderStatusIsCompleted = CheckRule(new OrderCannotBeConfirmedAfterCompletationRule(OrderStatus));

        if (cannotBeConfirmedWhenOrderStatusIsCompleted.IsError)
        {
            return cannotBeConfirmedWhenOrderStatusIsCompleted.FirstError;
        }

        OrderStatus = OrderStatus.Confirmed;
        ConfirmedOn = confirmedOn;

        Raise(new OrderConfirmedDomainEvent(
            Guid.NewGuid(),
            Id,
            confirmedOn));

        return Unit.Value;
    }

    public ErrorOr<Unit> Expire(DateTime expiredOn)
    {
        var cannotBeExpiredWhenOrderStatusIsExpired = CheckRule(new OrderCannotBeExpiredWhenOrderStatusIsExpiredRule(OrderStatus));

        if (cannotBeExpiredWhenOrderStatusIsExpired.IsError)
        {
            return cannotBeExpiredWhenOrderStatusIsExpired.FirstError;
        }

        var cannotExpiredAfterCompletation = CheckRule(new OrderCannotBeExpiredAfterCompletationRule(OrderStatus));

        if (cannotExpiredAfterCompletation.IsError)
        {
            return cannotExpiredAfterCompletation.FirstError;
        }

        OrderStatus = OrderStatus.Expired;
        ExpiredOn = expiredOn;

        Raise(new OrderExpiredDomainEvent(
            Guid.NewGuid(),
            Id,
            DateTime.UtcNow));

        return Unit.Value;
    }

    public ErrorOr<Unit> Pay(
        DateTime payedOn, 
        int actualStock, 
        StockStatus stockStatus)
    {
        var cannotBePayedWhenOrderStatusIsExpired = CheckRule(new OrderCannotBePayedWhenOrderStatusIsExpiredRule(OrderStatus));
        
        if (cannotBePayedWhenOrderStatusIsExpired.IsError)
        {
            return cannotBePayedWhenOrderStatusIsExpired.FirstError;
        }

        var cannotBePayedWhenOrderStatusIsNotConfirmed = CheckRule(new OrderCannotBePayedWhenOrderStatusIsNotConfirmedRule(OrderStatus));

        if (cannotBePayedWhenOrderStatusIsNotConfirmed.IsError)
        {
            return cannotBePayedWhenOrderStatusIsNotConfirmed.FirstError;
        }

        ErrorOr<Payment> pay = Payment.PayFromOrder(
            Id,
            TotalMoneyAmount,
            AmountOfItems,
            ItemId,
            CustomerId,
            actualStock,
            stockStatus);

        if (pay.IsError)
        {
            return pay.FirstError;
        }

        OrderStatus = OrderStatus.Payed;
        PayedOn = payedOn;

        Raise(new OrderPayedDomainEvent(
            Guid.NewGuid(),
            Id,
            ItemId,
            AmountOfItems,
            payedOn));

        return Unit.Value;
    }

    public ErrorOr<Unit> Complete(DateTime completedOn)
    {
        var cannotBeCompletedWhenOrderStatusIsNotPayed = CheckRule(new OrderCannotBeCompletedWhenOrderStatusIsNotPayedRule(OrderStatus));
        
        if (cannotBeCompletedWhenOrderStatusIsNotPayed.IsError)
        {
            return cannotBeCompletedWhenOrderStatusIsNotPayed.FirstError;
        }

        OrderStatus = OrderStatus.Completed;
        CompletedOn = completedOn;

        Raise(new OrderCompletedDomainEvent(
            Guid.NewGuid(),
            Id,
            CustomerId,
            completedOn));

        return Unit.Value;
    }

    private Order(){}
}
