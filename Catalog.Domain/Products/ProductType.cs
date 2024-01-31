using Newtonsoft.Json;

namespace Catalog.Domain.Products;

public sealed record ProductType
{
    public static ProductType TShirt => new ProductType(nameof(TShirt));

    public static ProductType Bag => new ProductType(nameof(Bag));

    public static ProductType Shirt => new ProductType(nameof(Shirt));

    public static ProductType Suit => new ProductType(nameof(TShirt));

    public static ProductType Sportswear => new ProductType(nameof(Sportswear));

    public static ProductType Jean => new ProductType(nameof(Jean));

    public static ProductType Pants => new ProductType(nameof(Pants));

    public static ProductType Shoes => new ProductType(nameof(Shoes));

    public static ProductType Jacket => new ProductType(nameof(Jacket));

    public static ProductType Coat => new ProductType(nameof(Coat));

    public static ProductType Dress => new ProductType(nameof(Dress));

    public static ProductType Underwear => new ProductType(nameof(Underwear));

    public static ProductType Accesories => new ProductType(nameof(Accesories));

    public string Value { get; private set; } = string.Empty;
    
    public static ProductType Create(string value)
    {
        return new ProductType(value);
    }

    [JsonConstructor]
    private ProductType(string value)
    {
        Value = value;
    }
}
