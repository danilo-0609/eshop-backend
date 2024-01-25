using BuildingBlocks.Application;
using FluentValidation;
using UserAccess.Domain.Users;
using UserAccess.Domain.Users.Errors;

namespace UserAccess.Application.Users.ChangeAddress;
internal sealed class ChangeAddressCommandValidator : AbstractValidator<ChangeEmailCommand>
{
    public ChangeAddressCommandValidator(IUserRepository userRepository, IExecutionContextAccessor executionContextAccessor)
    {
        RuleFor(r => r.Id)
            .NotNull();

        RuleFor(r => r.Address)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(r => r.Id)
            .MustAsync(async (id, _) =>
            {
                var user = await userRepository.GetByIdAsync(UserId.Create(id));


                if (user is null)
                {
                    return false;
                }

                return executionContextAccessor.UserId == user.Id.Value;

            }).WithMessage(UserErrorsCodes.CannotChangeAddress.Code);
    }
}