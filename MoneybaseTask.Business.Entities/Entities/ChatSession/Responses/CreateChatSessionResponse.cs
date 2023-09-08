// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="CreateChatSessionResponse.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Business.Entities.Entities.ChatSession.Responses;

public class CreateChatSessionResponse
{
    public CreateChatSessionResponse(string id)
    {
        ChatSessionId = id;
    }
    
    public string ChatSessionId { get; set; }
    public ChatSessionAgentDetailsResponse AgentDetails { get; set; }
}