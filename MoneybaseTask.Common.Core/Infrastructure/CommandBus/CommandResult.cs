// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="CommandResult.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using MoneybaseTask.Common.Core.Common.ErrorObject;

namespace MoneybaseTask.Common.Core.Infrastructure.CommandBus;

public class CommandResult : ICommandResult
{
    public bool Success { get; set; }

    public object Value { get; set; }

    public string NotificationMessage { get; set; }
        
    public List<ErrorItem> Errors { get; set; }

    public int StatusCode { get; set; } = StatusCodes.Status200OK;
}