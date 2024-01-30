using ErrorOr;

namespace Catalog.Domain.Comments;

public static class CommentErrorCodes
{
    public static Error NotFound =>
        Error.NotFound("Comment.NotFound", "Comment was not found");

    public static Error UserNoAuthorizedToAccess =>
        Error.Unauthorized("Comments.Unauthorized", "Cannot access to this content");
}
