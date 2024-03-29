using Catalog.Application.Common;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Comments.UpdateComment;

public sealed record UpdateCommentCommand(Guid CommentId, string Content) : ICommandRequest<ErrorOr<Unit>>;
