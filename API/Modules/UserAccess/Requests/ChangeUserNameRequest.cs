namespace API.Modules.UserAccess.Requests;

public sealed record ChangeUserNameRequest(
    string FirstName,
    string LastName);
