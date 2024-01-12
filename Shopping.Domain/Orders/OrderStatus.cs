using BuildingBlocks.Domain;

namespace Shopping.Domain.Orders;

public sealed record OrderStatus : ValueObject
{
    public static OrderStatus Placed => new OrderStatus(nameof(Placed));

    public static OrderStatus Confirmed => new OrderStatus(nameof(Confirmed));

    public static OrderStatus Expired => new OrderStatus(nameof(Expired));

    public static OrderStatus Payed => new OrderStatus(nameof(Payed));

    public static OrderStatus Completed => new OrderStatus(nameof(Completed));

    public string Value { get; set; }

    private OrderStatus(string value)
    {
        Value = value;
    }
}
