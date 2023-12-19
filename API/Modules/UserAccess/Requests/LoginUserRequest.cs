namespace API.Modules.UserAccess.Requests;

public sealed record LoginUserRequest(string Email, string Password);
