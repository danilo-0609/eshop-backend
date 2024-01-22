namespace Shopping.Domain.Items;

public sealed record StockStatus
{
    public static StockStatus WithStock => new StockStatus(nameof(WithStock));

    public static StockStatus OutOfStock => new StockStatus(nameof(OutOfStock));

    public string Value { get; private set; }

    public static StockStatus Create(string value)
    {
        return new StockStatus(value);
    }

    private StockStatus(string value)
    {
        Value = value;
    }

    private StockStatus()
    {
    }
}
