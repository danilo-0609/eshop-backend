using Catalog.Domain.Products.ValueObjects;

namespace Catalog.Application.Products;

public sealed record ProductResponse(Guid Id,
    Guid SellerId,
    string Name,
    decimal Price,
    string Description, 
    List<Size> Sizes,
    List<Color> Colors,
    string ProductType,
    List<string> Tags,
    int InStock,
    DateTime CreatedDateTime,
    DateTime? UpdatedDateTime);