using MediatR;

namespace UserAccess.Application.Common;

internal interface ICommandRequest<TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}
