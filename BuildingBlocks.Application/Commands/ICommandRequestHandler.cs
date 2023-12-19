using System;
using MediatR;

namespace BuildingBlocks.Application.Commands;
public interface ICommandRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : ICommandRequest<TResponse>
    where TResponse : notnull
{

}
