using BuildingBlocks.Domain;
using ErrorOr;
using Shopping.Domain.Items;
using Shopping.Domain.Orders;
using Shopping.Domain.Payments.DomainEvents;
using Shopping.Domain.Payments.Rules;

namespace Shopping.Domain.Payments;

public sealed class Payment : AggregateRoot<PaymentId, Guid>
{
    public new PaymentId Id { get; private set; }

    public Guid PayerId { get; private set; }

    public OrderId OrderId { get; private set; }

    public decimal MoneyAmount { get; private set; }

    public DateTime PayedOn { get; private set; }

    private Payment(
        PaymentId id, 
        Guid payerId, 
        OrderId orderId, 
        decimal moneyAmount, 
        DateTime payedOn)
    {
        Id = id;
        PayerId = payerId;
        OrderId = orderId;
        MoneyAmount = moneyAmount;
        PayedOn = payedOn;
    }

    internal static ErrorOr<Payment> PayFromOrder(
        OrderId orderId,
        decimal moneyAmount,
        int itemAmount,
        ItemId itemId,
        Guid payerId,
        int actualAmount,
        StockStatus stockStatus)
    {
        var payment = new Payment(
            PaymentId.CreateUnique(),
            payerId,
            orderId,
            moneyAmount,
            DateTime.UtcNow);

        var itemAmountGreaterThanActualAmount = payment.CheckRule(new PayCannotBeMadeWhenItemAmountRequestedIsGreaterThanItemCurrentAmountRule(itemAmount, actualAmount));
    
        if (itemAmountGreaterThanActualAmount.IsError)
        {
            return itemAmountGreaterThanActualAmount.FirstError;
        }

        var itemIsOutOfStock = payment.CheckRule(new PayCannotBeMadeWhenItemIsOutOfStockRule(stockStatus));

        if (itemIsOutOfStock.IsError)
        {
            return itemIsOutOfStock.FirstError;
        }

        payment.Raise(new PayMadeDomainEvent(
            Guid.NewGuid(),
            payment.Id,
            payment.OrderId,
            payment.PayerId,
            payment.MoneyAmount,
            itemId.Value,
            DateTime.UtcNow));

        return payment;
    }

    public static Payment Create(
        PaymentId paymentId,
        OrderId orderId,
        Guid payerId,
        decimal moneyAmount,
        DateTime payedOn)
    {
        return new Payment(paymentId, payerId, orderId, moneyAmount, payedOn);
    }

    private Payment()
    {
    }
}
