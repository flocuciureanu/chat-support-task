// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ChatSessionDetailsDocument.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Business.Entities.Entities.ChatSession.MongoModel;

public class ChatSessionDetailsDocument
{
    public string AgentId { get; set; }
    public ChatSessionUserDetailsDocument UserDetails { get; set; }
    public ChatSessionPollingDetailsDocument PollingDetails { get; set; }
    public IEnumerable<ChatSessionMessageDocument> Messages { get; set; }
}