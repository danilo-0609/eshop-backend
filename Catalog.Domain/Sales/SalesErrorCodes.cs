using ErrorOr;

namespace Catalog.Domain.Sales;

public static class SalesErrorCodes
{
    public static Error NotFound =>
        Error.NotFound("Sale.NotFound", "Sale was not found");

    public static Error CannotAccessToContent =>
        Error.Unauthorized("Sale.CannotAccess", "Cannot access to this content");
}
