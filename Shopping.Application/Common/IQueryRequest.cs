using MediatR;

namespace Shopping.Application.Common;

internal interface IQueryRequest<TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}
