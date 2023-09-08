// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ChatSessionModelBuilder.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Business.Entities.Entities.ChatSession.MongoModel;
using MoneybaseTask.Common.Core.Infrastructure.Builder.CollectionBuilder;
using MongoDB.Bson;

namespace MoneybaseTask.Business.Services.Features.ChatSession.Builders;

public class ChatSessionModelBuilder : CollectionBuilder<ChatSessionCollection>, IChatSessionModelBuilder
{
    public ChatSessionModelBuilder()
    {
        Reset();
    }

    public IChatSessionModelBuilder AddFirstName(string firstName)
    {
        this.Collection.Details.UserDetails.FirstName = firstName;
        return this;
    }

    public IChatSessionModelBuilder AddLastName(string lastName)
    {
        this.Collection.Details.UserDetails.LastName = lastName;
        return this;
    }

    public IChatSessionModelBuilder AddEmailAddress(string emailAddress)
    {
        this.Collection.Details.UserDetails.EmailAddress = emailAddress.ToLower();
        return this;
    }
    
    public IChatSessionModelBuilder AddStatus(ChatSessionStatus status, string notes)
    {
        this.Collection.StatusHistory.Add(new ChatSessionStatusDocument()
        {
            Status = status,
            UpdatedOn = DateTime.UtcNow,
            Notes = notes
        });

        return this;
    }
    
    protected sealed override void Reset()
    {
        this.Collection = new ChatSessionCollection()
        {
            Id = ObjectId.GenerateNewId().ToString(),
            CreatedOn = DateTime.UtcNow,
            Details = new ChatSessionDetailsDocument()
            {
                UserDetails = new ChatSessionUserDetailsDocument(),
                PollingDetails = new ChatSessionPollingDetailsDocument(),
                Messages = new List<ChatSessionMessageDocument>()
            },
            StatusHistory = new List<ChatSessionStatusDocument>()
        };
    }
}
