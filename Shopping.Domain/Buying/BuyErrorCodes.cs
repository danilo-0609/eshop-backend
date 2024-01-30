using ErrorOr;

namespace Shopping.Domain.Buying;

public static class BuyErrorCodes
{
    public static Error NotFound =>
        Error.NotFound("Buy.NotFound", "Buys were not found");

    public static Error UserNotAuthorizedToAccess =>
        Error.Unauthorized("Buy.Unauthorized", "The user is not authorized to access in this content");
}
