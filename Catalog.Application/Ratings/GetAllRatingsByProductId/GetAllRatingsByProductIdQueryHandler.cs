using Catalog.Application.Common;
using Catalog.Application.Ratings.Catalog.Application.Ratings.GetAllRatingsByProductId;
using Catalog.Domain.Products;
using Catalog.Domain.Products.Errors;
using Catalog.Domain.Ratings;
using ErrorOr;

namespace Catalog.Application.Ratings.GetAllRatingsByProductId;

internal sealed class GetAllRatingsByProductIdQueryHandler : IQueryRequestHandler<GetAllRatingsByProductIdQuery, ErrorOr<IReadOnlyList<RatingResponse>>>
{
    private readonly IRatingRepository _ratingRepository;
    private readonly IProductRepository _productRepository;

    public GetAllRatingsByProductIdQueryHandler(IRatingRepository ratingRepository, IProductRepository productRepository)
    {
        _ratingRepository = ratingRepository;
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<IReadOnlyList<RatingResponse>>> Handle(GetAllRatingsByProductIdQuery request, CancellationToken cancellationToken)
    {
        bool productExists = await _productRepository.ExistsAsync(ProductId.Create(request.ProductId), cancellationToken);
        
        if (productExists is false)
        {
            return ProductErrorCodes.NotFound;
        }

        List<Rating> ratings = await _ratingRepository.GetRatingsByProductIdAsync(ProductId.Create(request.ProductId));

        if (!ratings.Any())
        {
            return RatingErrorCodes.NotFound;
        }

        var response = new List<RatingResponse>();

        foreach (var rating in ratings)
        {
            var ratingResponse = new RatingResponse(
                rating.Id.Value,
                rating.Rate,
                rating.UserId,
                rating.ProductId.Value,
                rating.CreatedDateTime,
                rating.UpdatedDateTime,
                rating.Feedback);

            response.Add(ratingResponse); 
        }

        return response.AsReadOnly();
    } 
}