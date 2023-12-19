using BuildingBlocks.Domain;

namespace Shopping.Domain.Orders;
public sealed class Order : AggregateRoot<OrderId, Guid>
{
    public new OrderId Id { get; private set; }



    private Order(OrderId id)
    {
        Id = id;
    }

    private Order(){}
}