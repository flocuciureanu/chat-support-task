// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ForbiddenException.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Common.Core.Common.Exceptions;

public class ForbiddenException : ApplicationException
{
    public ForbiddenException(string message) 
        : base("Forbidden Exception", message)
    {
    }
}