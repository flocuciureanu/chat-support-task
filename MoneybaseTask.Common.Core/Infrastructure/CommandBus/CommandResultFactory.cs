// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="CommandResultFactory.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Common.Core.Common.ErrorObject;

namespace MoneybaseTask.Common.Core.Infrastructure.CommandBus;

public class CommandResultFactory : ICommandResultFactory
{
    public CommandResult Create(bool success, object value = null, List<ErrorItem> errors = null)
    {
        return new CommandResult { Value = value, Success = success, Errors = errors };
    }

    public CommandResult Create(bool success, string message, object value = null, List<ErrorItem> errors = null)
    {
        return new CommandResult { NotificationMessage = message, Success = success, Value = value, Errors = errors };
    }

    public CommandResult Create(bool success, int statusCode, string message = null, object value = null, List<ErrorItem> errors = null)
    {
        return new CommandResult { NotificationMessage = message, Success = success, Value = value, StatusCode = statusCode, Errors = errors };
    }
    
    public CommandResult Create(bool success, int statusCode, string message, List<ErrorItem> errors)
    {
        return new CommandResult { Success = success, StatusCode = statusCode, NotificationMessage = message, Errors = errors };
    }
}