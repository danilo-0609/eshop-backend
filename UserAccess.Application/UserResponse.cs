namespace UserAccess.Application;
public sealed record UserResponse(
    Guid UserId,
    string Login,
    string Name,
    string Email,
    string Role,
    string Address,
    DateTime CreatedDateTime);