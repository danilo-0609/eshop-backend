namespace API.Modules.Catalog.Requests;

public sealed record PublishProductRequest(string Name,
    decimal Price,
    string Description,
    string Size,
    string ProductType,
    List<string> Tags,
    int InStock,
    string Color = "");
