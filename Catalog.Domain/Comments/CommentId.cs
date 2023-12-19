using BuildingBlocks.Domain;

namespace Catalog.Domain.Comments;

public sealed record CommentId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    public static CommentId Create(Guid value) => new CommentId(value);

    public static CommentId CreateUnique() => new CommentId(Guid.NewGuid());

    private CommentId(Guid value)
        : base(value)
    {
        Value = value;
    }

    private CommentId() {}
}

