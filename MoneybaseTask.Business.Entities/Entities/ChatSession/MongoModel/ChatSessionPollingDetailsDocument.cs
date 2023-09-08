// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ChatSessionPollingDetailsDocument.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Business.Entities.Entities.ChatSession.MongoModel;

public class ChatSessionPollingDetailsDocument
{
    public int PollingCount { get; set; }
    public DateTime LastPollRequestReceivedAt { get; set; }
}