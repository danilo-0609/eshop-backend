using FluentValidation;

namespace UserAccess.Application.Users.Login;
internal sealed class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
{
    public LoginUserQueryValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress().WithMessage("Email address must be valid");

        RuleFor(r => r.Password)
            .NotEmpty()
            .NotNull();
    }
}