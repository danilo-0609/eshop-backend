using ErrorOr;
using UserAccess.Application.Common;

namespace UserAccess.Application.Users.Admin.AddAdminUser;
public sealed record AddAdminUserCommand(
    string Login,
    string Password,
    string FirstName,
    string LastName,
    string Email,
    string Address) : ICommandRequest<ErrorOr<Guid>>;