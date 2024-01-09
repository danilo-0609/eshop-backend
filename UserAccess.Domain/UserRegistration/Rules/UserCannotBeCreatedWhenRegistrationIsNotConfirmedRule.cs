using BuildingBlocks.Domain;
using ErrorOr;
using UserAccess.Domain.UserRegistrations.Errors;

namespace UserAccess.Domain.UserRegistrations.Rules;

internal class UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule : IBusinessRule
{
    private readonly UserRegistrationStatus _actualRegistrationStatus;

    public UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule(
            UserRegistrationStatus userRegistrationStatus)
    {
        _actualRegistrationStatus = userRegistrationStatus;
    }

    public Error Error => UserRegistrationErrors.RegistrationIsNotConfirmed;

    public bool IsBroken() => _actualRegistrationStatus != UserRegistrationStatus.Confirmed;

    public static string Message => "User cannot be created when registration is not confirmed";
}