using BuildingBlocks.Application.Commands;
using ErrorOr;

namespace Catalog.Application.Products.PublishProducts;
public sealed record PublishProductCommand(string Name,
    decimal Price,
    string Description,
    string Size,
    string ProductType,
    List<string> Tags,
    int InStock,
    string Color = "") : ICommandRequest<ErrorOr<Guid>>;