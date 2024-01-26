namespace API.Modules.Shopping.Requests;

public sealed record CreateWishRequest(
    Guid ItemId,
    string Name,
    bool IsPrivate);
