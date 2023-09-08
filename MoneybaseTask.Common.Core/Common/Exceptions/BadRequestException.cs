// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="BadRequestException.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Common.Core.Common.Exceptions;

public abstract class BadRequestException : ApplicationException
{
    protected BadRequestException(string message)
        : base("Bad Request", message)
    {
    }
}