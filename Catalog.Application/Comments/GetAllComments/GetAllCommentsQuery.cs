using Catalog.Application.Common;
using ErrorOr;

namespace Catalog.Application.Comments.GetAllComments;

public sealed record GetAllCommentsQuery(
    Guid ProductId) : IQueryRequest<ErrorOr<IReadOnlyList<CommentResponse>>>;
