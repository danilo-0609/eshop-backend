using FluentValidation;

namespace UserAccess.Application.Users.AddProfileImage;

internal sealed class AddProfileImageCommandValidator : AbstractValidator<AddProfileImageCommand>
{
    public AddProfileImageCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotNull();

        RuleFor(r => r.FilePath)
            .NotNull()
            .NotEmpty();

        RuleFor(r => r.FormFile)
            .NotNull();
    }
}
