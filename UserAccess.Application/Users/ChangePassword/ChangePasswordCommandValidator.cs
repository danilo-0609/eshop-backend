using BuildingBlocks.Application;
using FluentValidation;
using UserAccess.Domain.Users;

namespace UserAccess.Application.Users.ChangePassword;

internal sealed class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    private readonly IExecutionContextAccessor _executionContextAccessor;
    private readonly IUserRepository _userRepository;

    public ChangePasswordCommandValidator(IExecutionContextAccessor executionContextAccessor, IUserRepository userRepository)
    {
        _executionContextAccessor = executionContextAccessor;
        _userRepository = userRepository;

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

        RuleFor(r => r.Id)
        .MustAsync(async (id, _) =>
        {
            var user = await _userRepository.GetByIdAsync(UserId.Create(id));

            if (user is null)
            {
                return false;
            }

            return _executionContextAccessor.UserId == user.Id.Value;
        })
        .WithErrorCode("403")
        .WithMessage("Cannot change other's user password");
    }
}