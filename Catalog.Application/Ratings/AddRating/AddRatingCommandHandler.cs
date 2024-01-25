using BuildingBlocks.Application;
using Catalog.Application.Common;
using Catalog.Domain.Products;
using Catalog.Domain.Ratings;
using ErrorOr;

namespace Catalog.Application.Ratings.AddRating;
internal sealed class AddRatingCommandHandler : ICommandRequestHandler<AddRatingCommand, ErrorOr<Guid>>
{
    private readonly IProductRepository _productRepository;
    private readonly IRatingRepository _ratingRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public AddRatingCommandHandler(IRatingRepository ratingRepository, IExecutionContextAccessor executionContextAccessor, IProductRepository productRepository)
    {
        _ratingRepository = ratingRepository;
        _executionContextAccessor = executionContextAccessor;
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(AddRatingCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(ProductId.Create(command.ProductId));

        if (product is null)
        {
            return Error.NotFound("Product.NotFound", "Product was not found");
        }

        Rating rating = Rating.Create(
            command.Rate,
            _executionContextAccessor.UserId, 
            product.Id, 
            DateTime.UtcNow, 
            command.Feedback);

        await _ratingRepository.AddAsync(rating);

        return rating.Id.Value;
    }

}
