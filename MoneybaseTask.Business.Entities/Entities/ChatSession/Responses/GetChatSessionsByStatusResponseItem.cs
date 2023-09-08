// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="GetChatSessionsByStatusResponseItem.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Common.Core.Infrastructure.Builder.Mapper;

namespace MoneybaseTask.Business.Entities.Entities.ChatSession.Responses;

public class GetChatSessionsByStatusResponseItem : IMappable
{
    public ChatSessionDetailsResponse ChatSessionDetailsResponse { get; set; }
    public AgentDetailsResponse AgentDetailsResponse { get; set; }
    public UserDetailsResponse UserDetailsResponse { get; set; }
}