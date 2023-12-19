using FluentValidation;

namespace UserAccess.Application.Users.GetUserById;
internal sealed class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(r => r.Id)
            .NotNull();
    }
}