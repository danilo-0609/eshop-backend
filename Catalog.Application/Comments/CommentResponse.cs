using System;

namespace Catalog.Application.Comments;

public sealed record CommentResponse(Guid UserId,
    string Content,
    DateTime CreatedDateTime,
    DateTime? UpdatedDateTime = null);
