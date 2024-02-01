using ErrorOr;

namespace BuildingBlocks.Domain;

public interface IBusinessRule
{
    static string Message { get; } = string.Empty;
    
    Error Error { get; }
    
    bool IsBroken();
}

