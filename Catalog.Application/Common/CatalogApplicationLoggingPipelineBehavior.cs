using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Common;

internal sealed class CatalogApplicationLoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommandRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly ILogger<CatalogApplicationLoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public CatalogApplicationLoggingPipelineBehavior(
        ILogger<CatalogApplicationLoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, 
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
