using ErrorOr;
using FluentValidation;
using MediatR;
using FluentValidation.Results;

namespace UserAccess.Application.Common;
internal sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequest> _validator;

    public ValidationBehavior(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is null)
        {
            return await next();
        }

        ValidationResult? validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            return await next();
        }

        List<Error> errors = validationResult
                    .Errors
                    .ConvertAll(error => 
                        Error.Validation($"{error.PropertyName}", $"{error.ErrorMessage}"));
    
        return (dynamic)errors;
    }
}