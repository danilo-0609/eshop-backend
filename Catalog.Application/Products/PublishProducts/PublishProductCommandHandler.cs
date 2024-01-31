using BuildingBlocks.Application;
using Catalog.Domain.Products;
using Catalog.Application.Common;
using ErrorOr;

namespace Catalog.Application.Products.PublishProducts;

internal sealed class PublishProductCommandHandler : ICommandRequestHandler<PublishProductCommand, ErrorOr<Guid>>
{
    private readonly IProductRepository _productRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public PublishProductCommandHandler(IProductRepository productRepository, 
        IExecutionContextAccessor executionContextAccessor)
    {
        _productRepository = productRepository;
        _executionContextAccessor = executionContextAccessor;

    }

    public async Task<ErrorOr<Guid>> Handle(PublishProductCommand command, CancellationToken cancellationToken)
    {
        ProductType productType = ProductType.Create(command.ProductType);
        List<Tag> tags = new();

        foreach (string tag in command.Tags)
        {
            Tag _tag = Tag.Create(tag);

            tags.Add(_tag);
        }

        Product product = Product.Publish(_executionContextAccessor.UserId,
            command.Name,
            command.Price,
            command.Description,
            command.Size,
            productType,
            tags,
            command.InStock,
            DateTime.UtcNow,
            command.Color);

        await _productRepository.AddAsync(product);

        return product.Id.Value;
    }

}
