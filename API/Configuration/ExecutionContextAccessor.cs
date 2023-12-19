using BuildingBlocks.Application;

namespace API.Configuration;

public class ExecutionContextAccessor : IExecutionContextAccessor
{
    private readonly HttpContextAccessor _contextAccessor;

    public ExecutionContextAccessor(HttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public Guid UserId { 
        get
        {
            if (_contextAccessor
                .HttpContext?
                .User?
                .Claims?
                .SingleOrDefault(x => x.Type == "sub")
                .Value != null)
            {
                return Guid.Parse(_contextAccessor
                    .HttpContext
                    .User
                    .Claims
                    .SingleOrDefault(x => x.Type == "sub").Value);
            }

            throw new ApplicationException("User context is not available");
        }
    } 
}

