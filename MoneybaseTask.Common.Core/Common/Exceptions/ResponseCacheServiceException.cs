// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ResponseCacheServiceException.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Common.Core.Common.Exceptions;

public class ResponseCacheServiceException : ApplicationException
{
    public ResponseCacheServiceException(string message)
        : base("Response cache service exception", message)
    {
    }
}