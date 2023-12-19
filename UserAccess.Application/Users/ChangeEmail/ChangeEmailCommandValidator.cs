using FluentValidation;
using UserAccess.Domain.Users;

namespace UserAccess.Application.Users.ChangeEmail;
internal sealed class ChangeEmailCommandValidator : AbstractValidator<ChangeEmailCommand>
{
    private readonly IUserRepository _userRepository;

    public ChangeEmailCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(r => r.Id)
            .NotNull();
        
        RuleFor(r => r.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress().WithMessage("Must be a valid email address")
            .MustAsync(async (email, _) => 
            {
                return await _userRepository.IsEmailUniqueAsync(email);
            }).WithMessage("Email must be unique");
    }
}