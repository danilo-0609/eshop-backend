using Catalog.Application.Common;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Comments.AddComment;

public sealed record AddCommentCommand(Guid ProductId,
    string Content) : ICommandRequest<ErrorOr<Unit>>;

