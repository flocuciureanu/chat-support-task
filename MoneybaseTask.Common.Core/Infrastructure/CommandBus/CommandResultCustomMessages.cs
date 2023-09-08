// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="CommandResultCustomMessages.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Common.Core.Infrastructure.CommandBus;

public static class CommandResultCustomMessages
{
    public const string CreateChatSessionSuccess = "Chat session created successfully";
    public const string CreateChatSessionErrorNoAgents = "There are no agents available at the moment. Please try again later";
    public const string CreateChatSessionErrorDuplicateEmail = "There is already a Pending or In Progress chat associated to the provided email address";
    public const string ChatSessionNotFoundForId = "No chat session found for id: ";
    public const string ChatSessionPollErrorWrongStatus = "Error: Current chat session status should be 'In progress'";
    public const string ChatSessionPollErrorPollCountEqualTo = "Error: Chat session poll count equal to: ";
    public const string ChatSessionPollErrorLastPollRequestReceivedAt = "Error: Chat session last poll request received at: ";
    public const string NoChatSessionFound = "No chat session found for the above filter";
    public const string UpdateStatusChatSessionSuccess = "Chat session status updated successfully";
    public const string UpdateStatusChatSessionWrongStatus = "The only supported status for this operation are: 'Pending' and 'In Progress'";
}