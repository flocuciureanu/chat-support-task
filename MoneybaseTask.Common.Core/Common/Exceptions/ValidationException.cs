// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ValidationException.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Common.Core.Common.ErrorObject;

namespace MoneybaseTask.Common.Core.Common.Exceptions;

public sealed class ValidationException : ApplicationException
{
    public ValidationException(ICollection<ErrorItem> errors)
        : base("Validation Failure", "One or more validation errors occurred")
        => Errors = errors;

    public ICollection<ErrorItem> Errors { get; }
}