// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="UserDetailsResponseResolver.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using AutoMapper;
using MoneybaseTask.Business.Entities.Entities.ChatSession.MongoModel;
using MoneybaseTask.Business.Entities.Entities.ChatSession.Responses;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Mapper.Resolvers;

public class UserDetailsResponseResolver : IValueResolver<ChatSessionCollection, GetChatSessionsByStatusResponseItem, UserDetailsResponse>
{
    public UserDetailsResponse Resolve(ChatSessionCollection source, GetChatSessionsByStatusResponseItem destination,
        UserDetailsResponse destMember, ResolutionContext context)
    {
        return new UserDetailsResponse()
        {
            Name = $"{source.Details.UserDetails.FirstName} {source.Details.UserDetails.LastName}",
            Email = source.Details.UserDetails.EmailAddress
        };
    }
}