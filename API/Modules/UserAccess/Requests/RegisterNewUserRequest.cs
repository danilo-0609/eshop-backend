namespace API.Modules.UserAccess.Requests;

public sealed record RegisterNewUserRequest(string Login,
    string Password,
    string Email,
    string FirstName,
    string LastName,
    string Address);
