using FluentValidation;
using UserAccess.Domain.Users;

namespace UserAccess.Application.Users.Admin.AddAdminUser;

internal sealed class AddAdminUserCommandValidator : AbstractValidator<AddAdminUserCommand>
{
    private readonly IUserRepository _userRepository;
    public AddAdminUserCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(r => r.Login)
            .NotEmpty()
            .NotNull()
            .MaximumLength(40);

        RuleFor(r => r.Password)
            .NotEmpty()
            .NotNull()
            .MaximumLength(35);

        RuleFor(r => r.FirstName)
            .NotEmpty()
            .NotNull()
            .MaximumLength(30);

        RuleFor(r => r.LastName)
            .NotEmpty()
            .NotNull()
            .MaximumLength(30);
        
        RuleFor(r => r.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress().WithMessage("Must be a valid email address")
            .MustAsync(async (email, _) => 
            {
                return await _userRepository.IsEmailUniqueAsync(email);
            }).WithMessage("Email must be unique");
    
        RuleFor(r => r.Address)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50);
    }
}