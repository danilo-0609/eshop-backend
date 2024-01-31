namespace Catalog.Application.Products;

public sealed record ProductResponse(Guid Id,
    Guid SellerId,
    string Name,
    decimal Price,
    string Description, 
    string Size,
    string Color,
    string ProductType,
    List<string> Tags,
    int InStock,
    DateTime CreatedDateTime,
    DateTime? UpdatedDateTime);