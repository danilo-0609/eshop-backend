using Catalog.Application.Common;
using Catalog.Domain.Comments;
using Catalog.Domain.Products;
using Catalog.Domain.Products.Errors;
using ErrorOr;

namespace Catalog.Application.Comments.GetAllComments;
internal sealed class GetAllCommentsQueryHandler : IQueryRequestHandler<GetAllCommentsQuery, ErrorOr<IReadOnlyList<CommentResponse>>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IProductRepository _productRepository;

    public GetAllCommentsQueryHandler(ICommentRepository commentRepository, IProductRepository productRepository)
    {
        _commentRepository = commentRepository;
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<IReadOnlyList<CommentResponse>>> Handle(GetAllCommentsQuery query, CancellationToken cancellationToken)
    {
        bool productExists = await _productRepository.ExistsAsync(ProductId.Create(query.ProductId), cancellationToken);

        if (productExists is false)
        {
            return ProductErrorCodes.NotFound;
        }

        List<Comment>? comments = await _commentRepository.GetAllCommentsByProductIdAsync(ProductId.Create(query.ProductId));

        if (comments is null)
        {
            return CommentErrorCodes.NotFound;
        }

        List<CommentResponse> response = new();

        comments.ForEach(comment => 
        {
            CommentResponse commentResponse = new(comment.UserId, 
                comment.Content, 
                comment.CreatedDateTime, 
                comment.UpdatedDateTime);

            response.Add(commentResponse);
        });

        return response.AsReadOnly();
    }
}
