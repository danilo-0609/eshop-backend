using BuildingBlocks.Application;
using FluentValidation;
using UserAccess.Domain.Users;
using UserAccess.Domain.Users.Errors;

namespace UserAccess.Application.Users.ChangeLogin;
internal sealed class ChangeLoginCommandValidator : AbstractValidator<ChangeLoginCommand>
{
    public ChangeLoginCommandValidator(IUserRepository userRepository, IExecutionContextAccessor executionContextAccessor)
    {
        RuleFor(r => r.Id)
            .NotNull();

        RuleFor(r => r.Login)
            .NotNull()
            .NotEmpty()
            .MustAsync(async (login, _) => 
            {
                return await userRepository.IsLoginUniqueAsync(login);

            }).WithMessage("Login must be unique")
            .MaximumLength(40);
    }
}