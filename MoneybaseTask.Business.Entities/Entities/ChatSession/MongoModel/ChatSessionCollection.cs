// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ChatSessionCollection.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Common.Core.Infrastructure.Persistence;

namespace MoneybaseTask.Business.Entities.Entities.ChatSession.MongoModel;

public class ChatSessionCollection : BaseMongoCollection
{
    public ChatSessionDetailsDocument Details { get; set; }
    public DateTime CreatedOn { get; set; }
    public ICollection<ChatSessionStatusDocument> StatusHistory { get; set; }
}