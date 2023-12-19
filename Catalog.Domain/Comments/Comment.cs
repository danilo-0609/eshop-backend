using BuildingBlocks.Domain;
using Catalog.Domain.Products;

namespace Catalog.Domain.Comments;

public sealed class Comment : AggregateRoot<CommentId, Guid>
{
    public new CommentId Id { get; private set; }
    
    public Guid UserId { get; private set; }

    public ProductId ProductId { get; private set; }

    public string Content { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedDateTime { get; private set; }

    public DateTime? UpdatedDateTime { get; private set; }

    public static Comment Create(Guid userId,
        ProductId productId,
        string content,
        DateTime OcurredOn)
    {
        var comment = new Comment(
            CommentId.CreateUnique(),
            userId,
            productId,
            content,
            isActive: true,
            OcurredOn);

        return comment;
    }

    public static Comment Update(CommentId id,
        Guid userId,
        ProductId productId, 
        string content,
        DateTime createdOn,
        DateTime updatedOn)
    {
        var comment = new Comment(id,
            userId,
            productId,
            content,
            isActive: true,
            createdOn,
            updatedOn);

        return comment;
    }

    public void Remove()
    {
        IsActive = false;
    }

    private Comment(CommentId id,
        Guid userId,
        ProductId productId,
        string content,
        bool isActive,
        DateTime createdDateTime,
        DateTime? updatedDateTime = null) 
        : base(id)
    {
        Id = id;
        UserId = userId;
        ProductId = productId;
        Content = content;
        IsActive = isActive;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime; 
    }

    private Comment() {}
}
