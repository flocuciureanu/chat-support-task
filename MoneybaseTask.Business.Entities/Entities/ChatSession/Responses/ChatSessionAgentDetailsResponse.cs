// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ChatSessionAgentDetailsResponse.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Business.Entities.Entities.ChatSession.Responses;

public class ChatSessionAgentDetailsResponse
{
    public ChatSessionAgentDetailsResponse(string id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public string Id { get; set; }
    public string Name { get; set; }
}