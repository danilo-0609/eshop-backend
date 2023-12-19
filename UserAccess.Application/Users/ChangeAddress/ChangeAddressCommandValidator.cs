using FluentValidation;

namespace UserAccess.Application.Users.ChangeAddress;
internal sealed class ChangeAddressCommandValidator : AbstractValidator<ChangeAddressCommand>
{
    public ChangeAddressCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotNull();

        RuleFor(r => r.Address)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50);
    }
}