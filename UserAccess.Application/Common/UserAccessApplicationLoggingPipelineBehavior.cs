using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace UserAccess.Application.Common;
internal sealed class UserAccessApplicationLoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly ILogger<UserAccessApplicationLoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public UserAccessApplicationLoggingPipelineBehavior(ILogger<UserAccessApplicationLoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting request: {@RequestName} {@DateTimeUtc}",
            typeof(TRequest).Name,
            DateTime.UtcNow);

        var result = await next();
                
        if (result.IsError)
        {
            _logger.LogError(
                "Request failure {@RequestName}, {@Errors}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                result.Errors,
                DateTime.UtcNow);
        }

        _logger.LogInformation(
            "Successfully completed request {@RequestName} {@DateTimeUtc}",
            typeof(TRequest).Name,
            DateTime.UtcNow);

        return result;
    }
}
