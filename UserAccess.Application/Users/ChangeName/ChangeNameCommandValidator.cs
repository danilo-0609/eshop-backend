using BuildingBlocks.Application;
using FluentValidation;
using UserAccess.Domain.Users;
using UserAccess.Domain.Users.Errors;

namespace UserAccess.Application.Users.ChangeName;

internal sealed class ChangeNameCommandValidator : AbstractValidator<ChangeNameCommand>
{
    private readonly IExecutionContextAccessor _executionContextAccessor;
    private readonly IUserRepository _userRepository;

    public ChangeNameCommandValidator(IExecutionContextAccessor executionContextAccessor, IUserRepository userRepository)
    {
        _executionContextAccessor = executionContextAccessor;
        _userRepository = userRepository;

        RuleFor(r => r.Id)
            .NotNull();

        RuleFor(r => r.FirstName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(30);

        RuleFor(r => r.LastName)
            .NotNull()
            .MaximumLength(30);

        RuleFor(r => r.Id)
            .MustAsync(async (id, _) =>
            {
                var user = await _userRepository.GetByIdAsync(UserId.Create(id));

                if (user is null)
                {
                    return false;
                }

                return executionContextAccessor.UserId == user.Id.Value;
            })
            .WithMessage(UserErrorsCodes.CannotChangeName.Code);
    }
}