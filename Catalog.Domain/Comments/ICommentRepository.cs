using Catalog.Domain.Products;

namespace Catalog.Domain.Comments;
public interface ICommentRepository
{
    Task AddAsync(Comment comment);

    Task UpdateAsync(Comment comment);

    Task<Comment?> GetByIdAsync(CommentId commentId);

    Task DeleteAsync(CommentId id);       

    Task<List<Comment>?> GetAllCommentsByProductIdAsync(ProductId productId); 
}