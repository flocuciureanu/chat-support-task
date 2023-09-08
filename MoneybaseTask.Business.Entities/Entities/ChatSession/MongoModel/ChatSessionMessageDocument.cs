// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ChatSessionMessageDocument.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Business.Entities.Entities.ChatSession.MongoModel;

public class ChatSessionMessageDocument
{
    //Populating this with the AgentId and with the user email address 
    public string SenderId { get; set; }
    public string Content { get; set; }
    public DateTime SentAt { get; set; }
    public ChatMessageType Type { get; set; }
}