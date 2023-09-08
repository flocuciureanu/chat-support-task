// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="UnauthorizedException.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Common.Core.Common.Exceptions;

public class UnauthorizedException : ApplicationException
{
    public UnauthorizedException(string message) 
        : base("Unauthorized Exception", message)
    {
    }
}