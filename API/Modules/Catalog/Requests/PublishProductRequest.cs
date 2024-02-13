namespace API.Modules.Catalog.Requests;


public sealed record PublishProductRequest(string Name,
    decimal Price,
    string Description,
    List<string> Sizes,
    string ProductType,
    List<string> Tags,
    int InStock,
    List<string> Colors);
