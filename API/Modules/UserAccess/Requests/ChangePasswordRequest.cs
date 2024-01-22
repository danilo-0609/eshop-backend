namespace API.Modules.UserAccess.Requests;

public sealed record ChangePasswordRequest(
    string ActualPassword,
    string NewPassword);
