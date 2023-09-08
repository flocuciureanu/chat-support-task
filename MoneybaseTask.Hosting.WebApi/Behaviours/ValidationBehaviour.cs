// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ValidationBehaviour.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using FluentValidation;
using MediatR;
using MoneybaseTask.Common.Core.Common.ErrorObject;
using ValidationException = MoneybaseTask.Common.Core.Common.Exceptions.ValidationException;

namespace MoneybaseTask.Hosting.WebApi.Behaviours;

internal sealed class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    
    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators) 
        => _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
        var errorsDictionary = _validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .GroupBy(
                x => x.PropertyName,
                x => x.ErrorMessage,
                (propertyName, errorMessages) => new
                {
                    Key = propertyName,
                    Values = errorMessages.Distinct().ToArray()
                })
            .ToDictionary(x => x.Key, x => x.Values);
        
        if (!errorsDictionary.Any()) 
            return await next();
        
        var errors = errorsDictionary.Select(keyValuePair => new ErrorItem
        {
            Field = keyValuePair.Key, 
            Message = string.Join("|", keyValuePair.Value) 
        }).ToList();

        throw new ValidationException(errors);
    }
}