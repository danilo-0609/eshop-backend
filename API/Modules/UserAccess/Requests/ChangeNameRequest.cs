namespace API.Modules.UserAccess.Requests;

public sealed record ChangeNameRequest(
    string FirstName,
    string LastName);
