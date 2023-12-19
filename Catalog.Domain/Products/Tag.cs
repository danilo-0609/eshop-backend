using BuildingBlocks.Domain;

namespace Catalog.Domain.Products;

public sealed record Tag : ValueObject
{
    public string Value { get; private set; }

    public static Tag Create(string value) 
    {
        return new Tag(value);
    }

    private Tag(string value) 
    {
        Value = value;
    }
    
}

