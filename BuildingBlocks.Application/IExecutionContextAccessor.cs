namespace BuildingBlocks.Application;
public interface IExecutionContextAccessor
{
    Guid UserId { get; }

    bool IsAdmin { get; }
}
