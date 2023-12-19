using FluentValidation;
using UserAccess.Domain.Users;

namespace UserAccess.Application.Users.ChangeLogin;
internal sealed class ChangeLoginCommandValidator : AbstractValidator<ChangeLoginCommand>
{
    private readonly IUserRepository _userRepository;

    public ChangeLoginCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(r => r.Id)
            .NotNull();

        RuleFor(r => r.Login)
            .NotNull()
            .NotEmpty()
            .MustAsync(async (login, _) => 
            {
                return await _userRepository.IsLoginUniqueAsync(login);
            }).WithMessage("Login must be unique")
            .MaximumLength(40);
    }
}