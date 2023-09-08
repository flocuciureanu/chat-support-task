// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="IChatSessionModelBuilder.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Business.Entities.Entities.ChatSession.MongoModel;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Builders;

public interface IChatSessionModelBuilder
{
    IChatSessionModelBuilder AddFirstName(string firstName);
    IChatSessionModelBuilder AddLastName(string lastName);
    IChatSessionModelBuilder AddEmailAddress(string emailAddress);
    IChatSessionModelBuilder AddStatus(ChatSessionStatus status, string notes);
    ChatSessionCollection Build();
}