namespace UserAccess.Application;

public sealed record UserResponse(
    Guid UserId,
    string Login,
    string Name,
    string Email,
    List<string> Role,
    DateTime CreatedDateTime);