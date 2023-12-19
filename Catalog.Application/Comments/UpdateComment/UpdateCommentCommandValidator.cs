using FluentValidation;

namespace Catalog.Application.Comments.UpdateComment;
internal sealed class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentCommandValidator()
    {
        RuleFor(r => r.CommentId)
            .NotNull().WithMessage("Comment id cannot be null")
            .NotEmpty().WithMessage("Comment id cannot be empty");

        RuleFor(r => r.Content)
            .NotNull().WithMessage("Content cannot be null")
            .NotEmpty().WithMessage("Content cannot be empty")
            .MaximumLength(3000).WithMessage("Content too long (must be less than 3000 letters)");
    }

}
