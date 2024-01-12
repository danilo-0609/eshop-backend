namespace Shopping.Domain.Items;

public sealed record StockStatus
{
    public static StockStatus WithStock => new StockStatus(nameof(WithStock));

    public static StockStatus OutOfStock => new StockStatus(nameof(OutOfStock));

    public string Value { get; private set; }

    private StockStatus(string value)
    {
        Value = value;
    }
}
