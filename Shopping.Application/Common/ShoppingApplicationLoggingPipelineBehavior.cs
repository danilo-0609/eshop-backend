using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Shopping.Application.Common;

internal class ShoppingApplicationLoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest
    where TResponse : IErrorOr
{
    private readonly ILogger<ShoppingApplicationLoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public ShoppingApplicationLoggingPipelineBehavior(ILogger<ShoppingApplicationLoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting request: {@RequestName}, {@DateTimeUtc}",
            typeof(TRequest).Name,
            DateTime.UtcNow);

        var result = await next();

        if (result.IsError)
        {
            _logger.LogError(
                "Request failure {@RequestName}, {@Error}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                result.Errors,
                DateTime.UtcNow);
        }

        _logger.LogInformation("Completed request: {@RequestName}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                DateTime.UtcNow);


        return result;
    }
}
