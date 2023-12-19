using MediatR;

namespace BuildingBlocks.Application.Commands;

public interface ICommandRequest<TResponse> : IRequest<TResponse>
    where TResponse : notnull
{       
}
