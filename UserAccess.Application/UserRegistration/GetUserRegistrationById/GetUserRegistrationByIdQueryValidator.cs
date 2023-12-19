using FluentValidation;
using UserAccess.Application.UserRegistration.GetUserRegistrationByIda;

namespace UserAccess.Application.UserRegistration.GetUserRegistrationById;

internal sealed class GetUserRegistrationByIdQueryValidator : AbstractValidator<GetUserRegistrationByIdQuery>
{
    public GetUserRegistrationByIdQueryValidator()
    {
        RuleFor(r => r.UserRegistrationId)
            .NotNull();
    }
}
