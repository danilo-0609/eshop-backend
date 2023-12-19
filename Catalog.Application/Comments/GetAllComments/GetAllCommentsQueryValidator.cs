using FluentValidation;

namespace Catalog.Application.Comments.GetAllComments;
internal sealed class GetAllCommentsQueryValidator : AbstractValidator<GetAllCommentsQuery>
{
    public GetAllCommentsQueryValidator()
    {
        RuleFor(r => r.ProductId)
            .NotNull().WithMessage("Product id cannot be null")
            .NotEmpty().WithMessage("Product id cannot be empty");
    }
}
