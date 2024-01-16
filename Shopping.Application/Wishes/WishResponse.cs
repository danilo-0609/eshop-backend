namespace Shopping.Application.Wishes;

public sealed record WishResponse(
    Guid Id,
    List<Guid> ItemIds,
    string Name,
    bool IsPrivate,
    DateTime CreatedOn);