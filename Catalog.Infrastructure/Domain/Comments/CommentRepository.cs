using Catalog.Domain.Comments;
using Catalog.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Domain.Comments;
internal sealed class CommentRepository : ICommentRepository
{
    private readonly CatalogDbContext _dbContext;

    public CommentRepository(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Comment comment)
    {
        await _dbContext.Comments.AddAsync(comment);
    }

    public async Task DeleteAsync(CommentId id)
    {
        await _dbContext
            .Comments
            .Where(t => t.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<List<Comment>?> GetAllCommentsByProductIdAsync(ProductId productId)
    {
        List<Comment> comments = await _dbContext
            .Comments
            .Where(p => p.ProductId == productId)
            .ToListAsync();

        if (comments.Count == 0)
        {
            return null;
        }

        return comments;
    }

    public async Task<Comment?> GetByIdAsync(CommentId commentId)
    {
        return await _dbContext
            .Comments
            .Where(w => w.Id == commentId)
            .SingleOrDefaultAsync();
    }

    public async Task UpdateAsync(Comment comment)
    {
        await _dbContext
            .Comments
            .Where(f => f.Id == comment.Id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(s => s.Id, comment.Id)
                .SetProperty(s => s.UserId, comment.UserId)
                .SetProperty(s => s.ProductId, comment.ProductId)
                .SetProperty(s => s.Content, comment.Content)
                .SetProperty(s => s.IsActive, comment.IsActive)
                .SetProperty(s => s.CreatedDateTime, comment.CreatedDateTime)
                .SetProperty(s => s.UpdatedDateTime, comment.UpdatedDateTime));
    }
}