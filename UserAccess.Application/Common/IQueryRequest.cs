using MediatR;

namespace UserAccess.Application.Common;

internal interface IQueryRequest<TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}