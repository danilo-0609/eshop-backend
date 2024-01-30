using ErrorOr;

namespace Shopping.Domain.Wishes;

public static class WishErrorCodes
{
    public static Error NotFound =>
        Error.NotFound("Wish.NotFound", "Wish was not found");

    public static Error UserNotAuthorizedToAccess =>
        Error.Unauthorized("Wish.Unauthorized", "The user is not authorized to access in this content");
}
