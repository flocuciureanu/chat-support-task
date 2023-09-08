// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="NotFoundException.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Common.Core.Common.Exceptions;

public abstract class NotFoundException : ApplicationException
{
    protected NotFoundException(string message)
        : base("Not Found", message)
    {
    }
}