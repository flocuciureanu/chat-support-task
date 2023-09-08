// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="MarkChatSessionAsCompletedRequest.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Business.Entities.Entities.ChatSession.Requests;

public class MarkChatSessionAsCompletedRequest
{
    public string Id { get; set; }
    public string Notes { get; set; }
}