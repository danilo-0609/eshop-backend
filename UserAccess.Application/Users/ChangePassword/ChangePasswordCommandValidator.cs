using BuildingBlocks.Application;
using FluentValidation;
using UserAccess.Domain.Users;
using UserAccess.Domain.Users.Errors;

namespace UserAccess.Application.Users.ChangePassword;

internal sealed class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator(IExecutionContextAccessor executionContextAccessor, IUserRepository userRepository)
    {
        RuleFor(r => r.Id)
            .NotNull();

        RuleFor(r => r.OldPassword)
            .NotEmpty()
            .NotNull()
            .MaximumLength(35);

        RuleFor(r => r.NewPassword)
            .NotEmpty()
            .NotNull()
            .MaximumLength(35);
    }
}