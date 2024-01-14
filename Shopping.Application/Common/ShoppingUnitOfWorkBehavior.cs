using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;
using System.Transactions;

namespace Shopping.Application.Common;

internal sealed class ShoppingUnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommandRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IUnitOfWork _unitOfWork;

    public ShoppingUnitOfWorkBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var response = await next();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            transactionScope.Complete();

            return response;
        }
    }
}
