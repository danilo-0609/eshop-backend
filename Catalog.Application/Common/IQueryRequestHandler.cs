using MediatR;

namespace Catalog.Application.Common;

internal interface IQueryRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IQueryRequest<TResponse>
    where TResponse : notnull
{
}