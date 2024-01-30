using ErrorOr;

namespace UserAccess.Domain.Users.Errors;

public static class UserErrorsCodes
{
    public static Error NotFound =
        Error.NotFound(
            code: "User.NotFound", 
            description: "User was not found");

    public static Error IncorrectPassword =
        Error.Validation(
            code: "Password.Incorrect", 
            description: "The password is not correct");

    public static Error IncorrectOldPassword =>
        Error.Validation("Password.NotCorrect", "The actual password you introduced is not your actual password");

    public static Error CannotChangeName =>
        Error.Unauthorized("User.CannotChangeName", "Cannot change the other user's name");

    public static Error CannotChangeLogin =>
        Error.Unauthorized("User.CannotChangeLogin", "Cannot change the other user's login");

    public static Error CannotChangeEmail =>
        Error.Unauthorized("User.CannotChangeEmail", "Cannot change if you are not the same user");

    public static Error CannotChangeAddress =>
        Error.Unauthorized("User.CannotChangeAddress", "Cannot change if you're not the same user");

    public static Error CannotChangePassword =>
        Error.Unauthorized("User.CannotChangePassword", "Cannot change if you're not the same user");

    public static Error CannotRemove =>
        Error.Unauthorized("User.CannotRemove", "Cannot remove if you're not the same user or admin");
}
