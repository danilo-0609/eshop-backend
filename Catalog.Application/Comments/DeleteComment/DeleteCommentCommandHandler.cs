using Catalog.Application.Common;
using Catalog.Domain.Comments;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Comments.DeleteComment;
internal sealed class DeleteCommentCommandHandler : ICommandRequestHandler<DeleteCommentCommand, ErrorOr<Unit>>
{
    private readonly ICommentRepository _commentRepository;

    public DeleteCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteCommentCommand command, CancellationToken cancellationToken)
    {
        Comment? comment = await _commentRepository.GetByIdAsync(CommentId.Create(command.Id));

        if (comment is null)
        {
            return Error.NotFound("Comment.NotFound", "Comment was not found");
        }

        await _commentRepository.DeleteAsync(comment.Id);

        return Unit.Value;
    }
}
