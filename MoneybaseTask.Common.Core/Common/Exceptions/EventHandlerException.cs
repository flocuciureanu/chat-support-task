// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="EventHandlerException.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Common.Core.Common.Exceptions;

public class EventHandlerException : ApplicationException
{
    public EventHandlerException(string message) 
        : base("Event handler exception", message)
    {
    }
    
    public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }
}