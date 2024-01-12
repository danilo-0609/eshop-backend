using BuildingBlocks.Domain;
using Shopping.Domain.Items;

namespace Shopping.Domain.Orders;

public sealed class Order : AggregateRoot<OrderId, Guid>
{
    public new OrderId Id { get; private set; }

    public Guid CustomerId { get; private set; }

    public ItemId ItemId { get; private set; }

    public OrderStatus OrderStatus { get; private set; }

    public DateTime PlacedOn { get; private set; }

    public DateTime? ConfirmedOn { get; private set; }

    public DateTime? ExpiredOn { get; private set; }

    private Order(
        OrderId id, 
        Guid customerId, 
        ItemId itemId, 
        OrderStatus orderStatus, 
        DateTime placedOn, 
        DateTime? confirmedOn = null, 
        DateTime? expiredOn = null)
    {
        Id = id;
        CustomerId = customerId;
        ItemId = itemId;
        OrderStatus = orderStatus;
        PlacedOn = placedOn;
        ConfirmedOn = confirmedOn;
        ExpiredOn = expiredOn;
    }

    public static Order Place(
        ItemId itemId,
        Guid customerId,
        DateTime placedOn,
        int amountOfProducts,
        int actualAmountOfProducts,
        StockStatus stockStatus)
    {
        var order = new Order(
            OrderId.CreateUnique(),
            customerId,
            itemId,
            OrderStatus.Placed,
            DateTime.UtcNow);

        order.CheckRule();
    }

    private Order(){}
}
