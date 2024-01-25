using MediatR;

namespace Shopping.Application.Common;

internal interface IQueryRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IQueryRequest<TResponse>
    where TResponse : notnull
{
}
