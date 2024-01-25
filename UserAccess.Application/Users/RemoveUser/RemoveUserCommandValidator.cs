using BuildingBlocks.Application;
using FluentValidation;
using UserAccess.Domain;
using UserAccess.Domain.Users;

namespace UserAccess.Application.Users.RemoveUser;

internal sealed class RemoveUserCommandValidator : AbstractValidator<RemoveUserCommand>
{
    public RemoveUserCommandValidator(IUserRepository userRepository, IExecutionContextAccessor executionContextAccessor)
    {
        RuleFor(r => r.UserId)
            .NotNull();

        RuleFor(r => r.UserId)
            .MustAsync(async (id, _) =>
            {
                var user = await userRepository.GetByIdAsync(UserId.Create(id));

                var userContext = await userRepository.GetByIdAsync(UserId.Create(executionContextAccessor.UserId));

                if (user is null)
                {
                    return false;
                }

                if (userContext!.Roles.Contains(Role.Administrator))
                {
                    return true;
                }

                return executionContextAccessor.UserId == user.Id.Value;
            })
            .WithMessage("Cannot remove if you're not the same user");
    }
}
