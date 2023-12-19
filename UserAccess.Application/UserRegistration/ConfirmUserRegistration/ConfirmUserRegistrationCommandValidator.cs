using FluentValidation;

namespace UserAccess.Application.UserRegistration.ConfirmUserRegistration;
internal sealed class ConfirmUserRegistrationCommandValidator : AbstractValidator<ConfirmUserRegistrationCommand>
{
    public ConfirmUserRegistrationCommandValidator()
    {
        RuleFor(r => r.UserRegistrationId)
            .NotNull().WithMessage("User registration id is required");
    }
}