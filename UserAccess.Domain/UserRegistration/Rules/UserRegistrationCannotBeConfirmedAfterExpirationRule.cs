using BuildingBlocks.Domain;
using ErrorOr;
using UserAccess.Domain.UserRegistrations.Errors;

namespace UserAccess.Domain.UserRegistrations.Rules;
internal sealed class UserRegistrationCannotBeConfirmedAfterExpirationRule : IBusinessRule
{
    private readonly UserRegistrationStatus _actualRegistrationStatus;

    public UserRegistrationCannotBeConfirmedAfterExpirationRule(
        UserRegistrationStatus userRegistrationStatus)
    {
        _actualRegistrationStatus = userRegistrationStatus;
    }

    public Error Error => UserRegistrationErrors.ConfirmedAfterExpiration;

    public bool IsBroken() => _actualRegistrationStatus == UserRegistrationStatus.Expired;

    public static string Message => "User registration cannot be confirmed after expiration";
}