using FluentValidation;

namespace UserAccess.Application.Users.RemoveUser;

internal sealed class RemoveUserCommandValidator : AbstractValidator<RemoveUserCommand>
{
    public RemoveUserCommandValidator()
    {
        RuleFor(r => r.UserId)
            .NotNull();
    }
}
