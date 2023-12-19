using System;
using MediatR;

namespace BuildingBlocks.Application.Queries;

public interface IQueryRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IQueryRequest<TResponse>
    where TResponse : notnull
{       
}
