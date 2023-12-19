using BuildingBlocks.Application;
using BuildingBlocks.Application.Commands;
using Catalog.Domain.Comments;
using Catalog.Domain.Products;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Comments.AddComment;
internal sealed class AddCommentCommandHandler : ICommandRequestHandler<AddCommentCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public AddCommentCommandHandler(IProductRepository productRepository,
        ICommentRepository commentRepository,
        IExecutionContextAccessor executionContextAccessor)
    {
        _commentRepository = commentRepository;
        _productRepository = productRepository; 
        _executionContextAccessor = executionContextAccessor;
    }

    public async Task<ErrorOr<Unit>> Handle(AddCommentCommand command, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(ProductId.Create(command.ProductId));

        if (product is null)
        {
            return Error.NotFound("Product.NotFound", "Product was not found");            
        }

        Comment comment = Comment.Create(
                _executionContextAccessor.UserId,
                product.Id,
                command.Content,
                DateTime.UtcNow);

        await _commentRepository.AddAsync(comment);

        return Unit.Value;
    }

}
