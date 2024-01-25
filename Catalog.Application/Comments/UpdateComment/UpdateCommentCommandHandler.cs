using Catalog.Application.Common;
using Catalog.Domain.Comments;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Comments.UpdateComment;

internal sealed class UpdateCommentCommandHandler : ICommandRequestHandler<UpdateCommentCommand, ErrorOr<Unit>>
{
    private readonly ICommentRepository _commentRepository;

    public UpdateCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateCommentCommand command, CancellationToken cancellationToken)
    {
        Comment? comment = await _commentRepository.GetByIdAsync(CommentId.Create(command.CommentId));
    
        if (comment is null)
        {
            return Error.NotFound("Comment.NotFound", "Comment was not found");
        }

        Comment update = Comment.Update(comment.Id,
            comment.UserId,
            comment.ProductId,
            command.Content,
            comment.CreatedDateTime,
            DateTime.UtcNow);

        await _commentRepository.UpdateAsync(update);

        return Unit.Value;
    }
}
