// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ChatSessionDetailsResolver.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using AutoMapper;
using MoneybaseTask.Business.Entities.Entities.ChatSession.MongoModel;
using MoneybaseTask.Business.Entities.Entities.ChatSession.Responses;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Mapper.Resolvers;

public class ChatSessionDetailsResolver : IValueResolver<ChatSessionCollection, GetChatSessionsByStatusResponseItem, ChatSessionDetailsResponse>
{
    public ChatSessionDetailsResponse Resolve(ChatSessionCollection source, GetChatSessionsByStatusResponseItem destination,
        ChatSessionDetailsResponse destMember, ResolutionContext context)
    {

        var lastStatus = source.StatusHistory.Last();
        return new ChatSessionDetailsResponse()
        {
            ChatId = source.Id,
            CreatedOn = source.CreatedOn.ToString("G"),
            StatusUpdatedOn = lastStatus.UpdatedOn.ToString("G"),
            Status = lastStatus.Status.ToString("G"),
            StatusNotes = lastStatus.Notes
        };
    }
}