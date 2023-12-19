using BuildingBlocks.Domain;
using Catalog.Domain.Products;

namespace Catalog.Domain.Ratings;
public sealed class Rating : AggregateRoot<RatingId, Guid>
{
    public new RatingId Id { get; private set; }

    public string Feedback { get; private set; }

    public Guid UserId { get; private set; }

    public ProductId ProductId { get; private set; }

    public double Rate { get; private set; }

    public DateTime CreatedDateTime { get; private set; }

    public DateTime? UpdatedDateTime { get; private set; }


    public static Rating Create(double rate,
        Guid userId,
        ProductId productId,
        DateTime createdOn,
        string feedback = "")
    {
        return new Rating(RatingId.CreateUnique(), 
            feedback, 
            userId, 
            productId, 
            rate, 
            createdOn);
    }

    public static Rating Update(RatingId ratingId,
        Guid userId,
        double rate,
        ProductId productId,
        DateTime createdOn,
        DateTime updatedOn,
        string feedback = "")
    {
        return new Rating(ratingId,
            feedback,
            userId,
            productId,
            rate,
            createdOn,
            updatedOn);
    }

    public Rating(RatingId id,
        string feedback,
        Guid userId,
        ProductId productId,
        double rate,
        DateTime createdDateTime,
        DateTime? updatedDateTime = null) 
        : base(id)
    {
        Id = id;
        Feedback = feedback;
        UserId = userId;
        ProductId = productId;
        Rate = rate;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public Rating()
    {
        
    }
}
