using BuildingBlocks.Domain;
using Newtonsoft.Json;

namespace Catalog.Domain.Products.ValueObjects;

public sealed record Tag : ValueObject
{
    public string Value { get; private set; }

    public static Tag Create(string value)
    {
        return new Tag(value);
    }

    [JsonConstructor]
    private Tag(string value)
    {
        Value = value;
    }

}

