using FluentValidation;

namespace UserAccess.Application.Users.ChangePassword;
internal sealed class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotNull();

        RuleFor(r => r.Password)
            .NotEmpty()
            .NotNull()
            .MaximumLength(35); 
    }
}