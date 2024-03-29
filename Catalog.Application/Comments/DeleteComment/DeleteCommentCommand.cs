using Catalog.Application.Common;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Comments.DeleteComment;

public sealed record DeleteCommentCommand(Guid Id) : ICommandRequest<ErrorOr<Unit>>;
