using MediatR;

namespace BuildingBlocks.Application.Queries;

public interface IQueryRequest<TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}

