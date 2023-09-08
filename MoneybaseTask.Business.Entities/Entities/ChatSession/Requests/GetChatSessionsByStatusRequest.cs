// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="GetChatSessionsByStatusRequest.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Business.Entities.Entities.ChatSession.MongoModel;

namespace MoneybaseTask.Business.Entities.Entities.ChatSession.Requests;

public class GetChatSessionsByStatusRequest
{
    public ChatSessionStatus Status { get; set; }
}