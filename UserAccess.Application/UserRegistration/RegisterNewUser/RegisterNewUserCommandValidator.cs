using FluentValidation;

namespace UserAccess.Application.UserRegistration.RegisterNewUser;

internal sealed class RegisterNewUserCommandValidator : AbstractValidator<RegisterNewUserCommand>
{
    public RegisterNewUserCommandValidator()
    {
        RuleFor(r => r.Login)
            .NotEmpty().WithMessage("Login cannot be empty")
            .NotNull().WithMessage("Login cannot be null")
            .MaximumLength(70).WithMessage("Login too large");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("Password cannot be empty")
            .NotNull().WithMessage("Password cannot be null")
            .MaximumLength(25).WithMessage("Password too large");

        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("Email cannot be empty")
            .NotNull().WithMessage("Email cannot be null")
            .EmailAddress().WithMessage("Email must be a valid email address");

        RuleFor(r => r.FirstName)
            .NotEmpty().WithMessage("First name cannot be empty")
            .NotNull().WithMessage("First name cannot be null")
            .MaximumLength(70).WithMessage("First name too large");

        RuleFor(r => r.LastName)
            .NotEmpty().WithMessage("Last name cannot be empty")
            .NotNull().WithMessage("Last name cannot be null")
            .MaximumLength(70).WithMessage("Last name too large");

        RuleFor(r => r.Address)
            .NotEmpty().NotNull()
            .MaximumLength(100).WithMessage("Address length too large");
    }
}
