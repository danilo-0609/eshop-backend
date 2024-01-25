using Catalog.Application.Common;
using Catalog.Domain.Comments;
using Catalog.Domain.Products;
using ErrorOr;

namespace Catalog.Application.Comments.GetAllComments;
internal sealed class GetAllCommentsQueryHandler : IQueryRequestHandler<GetAllCommentsQuery, ErrorOr<IReadOnlyList<CommentResponse>>>
{
    private readonly ICommentRepository _commentRepository;

    public GetAllCommentsQueryHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }


    public async Task<ErrorOr<IReadOnlyList<CommentResponse>>> Handle(GetAllCommentsQuery query, CancellationToken cancellationToken)
    {
        List<Comment>? comments = await _commentRepository.GetAllCommentsByProductIdAsync(ProductId.Create(query.ProductId));

        if (comments is null)
        {
            return Error.NotFound("Product.NotFound", "The comments in the product id were not found because the product was not found");
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
