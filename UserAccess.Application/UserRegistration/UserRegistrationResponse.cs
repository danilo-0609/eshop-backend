namespace UserAccess.Application.UserRegistration;

public sealed record UserRegistrationResponse(
    Guid Id,
    string Login,
    string Email,
    string Name,
    DateTime RegisteredDate,
    string Status,
    DateTime? ConfirmDate);
