using BuildingBlocks.Application;
using FluentValidation;
using UserAccess.Domain.Users;
using UserAccess.Domain.Users.Errors;

namespace UserAccess.Application.Users.ChangeEmail;

internal sealed class ChangeEmailCommandValidator : AbstractValidator<ChangeEmailCommand>
{
    public ChangeEmailCommandValidator(IUserRepository userRepository, IExecutionContextAccessor executionContextAccessor)
    {
        RuleFor(r => r.Id)
            .NotNull();
        
        RuleFor(r => r.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress().WithMessage("Must be a valid email address")
            .MustAsync(async (email, _) => 
            {
                return await userRepository.IsEmailUniqueAsync(email);
            }).WithMessage("Email must be unique");

        RuleFor(r => r.Id)
            .MustAsync(async (id, _) =>
            {
                var user = await userRepository.GetByIdAsync(UserId.Create(id));


                if (user is null)
                {
                    return false;
                }

                return executionContextAccessor.UserId == user.Id.Value;

            }).WithMessage(UserErrorsCodes.CannotChangeEmail.Code);

    }
}