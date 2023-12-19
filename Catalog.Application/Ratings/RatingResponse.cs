namespace Catalog.Application.Ratings;
public sealed record RatingResponse(
    Guid Id, 
    double Rate, 
    Guid UserId,
    Guid ProductId,
    DateTime CreatedDateTime, 
    DateTime? UpdatedDateTime, 
    string Feedback = "");