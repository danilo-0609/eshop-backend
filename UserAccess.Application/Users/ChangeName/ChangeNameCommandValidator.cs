using FluentValidation;

namespace UserAccess.Application.Users.ChangeName;
internal sealed class ChangeNameCommandValidator : AbstractValidator<ChangeNameCommand>
{
    public ChangeNameCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotNull();

        RuleFor(r => r.FirstName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(30);

        RuleFor(r => r.LastName)
            .NotNull()
            .MaximumLength(30);
    }
}