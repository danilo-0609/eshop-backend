using BuildingBlocks.Application;
using Catalog.Domain.Products;
using Catalog.Application.Common;
using ErrorOr;
using Catalog.Domain.Products.ValueObjects;

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
        List<Tag> tags = command.Tags.ConvertAll(tag => Tag.Create(tag));
        List<Size> sizes = command.Sizes.ConvertAll(size => new Size(size));
        List<Color> colors = command.Colors.ConvertAll(color => new Color(color));


        ErrorOr<Product> product = Product.Publish(_executionContextAccessor.UserId,
            command.Name,
            command.Price,
            command.Description,
            sizes,
            productType,
            tags,
            command.InStock,
            DateTime.UtcNow,
            colors);

        if (product.IsError)
        {
            return product.FirstError;
        }

        await _productRepository.AddAsync(product.Value);

        return product.Value.Id.Value;
    }

}
