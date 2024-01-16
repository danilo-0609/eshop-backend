using FluentValidation;

namespace Shopping.Application.Wishes.Create;

internal sealed class CreateWishListCommandValidator : AbstractValidator<CreateWishListCommand>
{
    public CreateWishListCommandValidator()
    {
        RuleFor(r => r.ItemId)
            .NotNull();

        RuleFor(r => r.Name)
            .NotEmpty();

        RuleFor(r => r.IsPrivate)
            .NotNull();
    }
}
