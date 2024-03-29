using BuildingBlocks.Domain;
using ErrorOr;
using UserAccess.Domain.UserRegistrations.Errors;

namespace UserAccess.Domain.UserRegistrations.Rules;
internal sealed class UserLoginMustBeUniqueRule : IBusinessRule
{
    private readonly string _login;
    private readonly IUsersCounter _usersCounter;


    public UserLoginMustBeUniqueRule(string login, IUsersCounter usersCounter)
    {
        _login = login;
        _usersCounter = usersCounter;
    }

    public Error Error => UserRegistrationErrors.LoginIsNotUnique;

    public bool IsBroken() => _usersCounter.CountUsersWithLogin(_login) > 0;

    public static string Message => "User login must be unique";
}