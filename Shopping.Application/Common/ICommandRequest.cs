using MediatR;

namespace Shopping.Application.Common;

internal interface ICommandRequest<TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}
