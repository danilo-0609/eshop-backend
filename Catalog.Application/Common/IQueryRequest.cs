using MediatR;

namespace Catalog.Application.Common;

internal interface IQueryRequest<TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}
