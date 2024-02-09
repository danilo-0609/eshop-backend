using ErrorOr;

namespace Shopping.Domain.Items;

public static class ItemErrorCodes
{
    public static Error NotFound => Error.NotFound("Item.NotFound", "Item was not found");
}
