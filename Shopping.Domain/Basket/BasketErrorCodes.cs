using ErrorOr;

namespace Shopping.Domain.Basket;

public static class BasketErrorCodes
{
    public static Error NotFound =>
        Error.NotFound("Basket.NotFound", "Basket was not found");

    public static Error UserNotAuthorizedToAccess =>
        Error.Unauthorized("Basket.Unauthorized", "The user is not authorized to access in this content");
}
