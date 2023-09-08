// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ICommandResult.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Common.Core.Common.ErrorObject;

namespace MoneybaseTask.Common.Core.Infrastructure.CommandBus;

public interface ICommandResult
{
    bool Success { get; set; }

    object Value { get; set; }

    string NotificationMessage { get; set; }

    List<ErrorItem> Errors { get; set; }
        
    int StatusCode { get; set; }
}