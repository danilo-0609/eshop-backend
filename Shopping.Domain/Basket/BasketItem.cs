namespace Shopping.Domain.Basket;

public sealed class BasketItem
{
    public Guid BasketId { get; set; }

    public Guid ItemId { get; set;  }

    public int AmountPerItem { get; set; }
}
