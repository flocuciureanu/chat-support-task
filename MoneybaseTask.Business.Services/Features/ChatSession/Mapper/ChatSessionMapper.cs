// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ChatSessionMapper.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using AutoMapper;
using MoneybaseTask.Business.Entities.Entities.ChatSession.MongoModel;
using MoneybaseTask.Business.Entities.Entities.ChatSession.Responses;
using MoneybaseTask.Business.Entities.Entities.ChatSession.Responses.Builders;
using MoneybaseTask.Business.Services.Features.ChatSession.Mapper.Resolvers;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Mapper;

public class ChatSessionMapper : Profile
{
    public ChatSessionMapper()
    {
        CreateMap<ChatSessionCollection, GetChatSessionsByStatusResponseItem>()
            .ForMember(d => d.ChatSessionDetailsResponse, e => e.MapFrom<ChatSessionDetailsResolver>())
            .ForMember(d => d.UserDetailsResponse, e => e.MapFrom<UserDetailsResponseResolver>())
            .ForMember(d => d.AgentDetailsResponse, e => e.MapFrom((_, _, _, context) =>
                context.Items[ChatSessionMappingOptionsConstants.GetChatSessionsByStatusResponse.AgentDetailsResponse]));
    }
}