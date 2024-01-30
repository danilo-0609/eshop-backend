using Catalog.Application.Common;
using Catalog.Domain.Comments;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Comments.DeleteComment;

internal sealed class DeleteCommentCommandHandler : ICommandRequestHandler<DeleteCommentCommand, ErrorOr<Unit>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IAuthorizationService _authorizationService;

    public DeleteCommentCommandHandler(ICommentRepository commentRepository, IAuthorizationService authorizationService)
    {
        _commentRepository = commentRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteCommentCommand command, CancellationToken cancellationToken)
    {
        Comment? comment = await _commentRepository.GetByIdAsync(CommentId.Create(command.Id));

        if (comment is null)
        {
            return CommentErrorCodes.NotFound;
        }

        var authorizationService = _authorizationService.IsUserAuthorized(comment.UserId);

        if (authorizationService.IsError && _authorizationService.IsAdmin() is false)
        {
            return CommentErrorCodes.UserNoAuthorizedToAccess;
        }

        await _commentRepository.DeleteAsync(comment.Id);

        return Unit.Value;
    }
}
