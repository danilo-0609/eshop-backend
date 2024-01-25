using MediatR;

namespace Catalog.Application.Common;

internal interface ICommandRequest<TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}

