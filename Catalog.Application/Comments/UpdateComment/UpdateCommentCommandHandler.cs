using Catalog.Application.Common;
using Catalog.Domain.Comments;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Comments.UpdateComment;

internal sealed class UpdateCommentCommandHandler : ICommandRequestHandler<UpdateCommentCommand, ErrorOr<Unit>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IAuthorizationService _authorizationService;

    public UpdateCommentCommandHandler(ICommentRepository commentRepository, IAuthorizationService authorizationService)
    {
        _commentRepository = commentRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateCommentCommand command, CancellationToken cancellationToken)
    {
        Comment? comment = await _commentRepository.GetByIdAsync(CommentId.Create(command.CommentId));
    
        if (comment is null)
        {
            return CommentErrorCodes.NotFound;
        }

        var authorizationService = _authorizationService.IsUserAuthorized(comment.UserId);

        if (authorizationService.IsError)
        {
            return CommentErrorCodes.UserNoAuthorizedToAccess;
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
