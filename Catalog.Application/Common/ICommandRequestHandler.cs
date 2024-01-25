using MediatR;

namespace Catalog.Application.Common;

internal interface ICommandRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : ICommandRequest<TResponse>
    where TResponse : notnull
{
}