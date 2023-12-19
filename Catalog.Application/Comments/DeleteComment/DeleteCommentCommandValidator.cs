using FluentValidation;

namespace Catalog.Application.Comments.DeleteComment;
internal sealed class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
{
    public DeleteCommentCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotNull().WithMessage("Comment id cannot be null")
            .NotEmpty().WithMessage("Comment id cannot be empty");
    }
}

