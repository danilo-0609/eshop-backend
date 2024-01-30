using ErrorOr;

namespace Catalog.Domain.Ratings;

public static class RatingErrorCodes
{
    public static Error NotFound =>
        Error.NotFound("Rating.NotFound", "Rating was not found");

    public static Error CannotAccessToContent =>
        Error.Unauthorized("Rating.CannotAccess", "Cannot access to this content");
}
