using Catalog.Application.Common;
using ErrorOr;

namespace Catalog.Application.Products.PublishProducts;

public sealed record PublishProductCommand(string Name,
    decimal Price,
    string Description,
    List<string> Sizes,
    string ProductType,
    List<string> Tags,
    int InStock,
    List<string> Colors) : ICommandRequest<ErrorOr<Guid>>;