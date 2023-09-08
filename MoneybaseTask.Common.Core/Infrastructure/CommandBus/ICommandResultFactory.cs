// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ICommandResultFactory.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Common.Core.Common.ErrorObject;

namespace MoneybaseTask.Common.Core.Infrastructure.CommandBus;

public interface ICommandResultFactory
{
    CommandResult Create(bool success, object value = null, List<ErrorItem> errors = null);

    CommandResult Create(bool success, string message, object value = null, List<ErrorItem> errors = null);
        
    CommandResult Create(bool success, int statusCode, string message = null, object value = null, List<ErrorItem> errors = null);
    
    CommandResult Create(bool success, int statusCode, string message, List<ErrorItem> errors);
}