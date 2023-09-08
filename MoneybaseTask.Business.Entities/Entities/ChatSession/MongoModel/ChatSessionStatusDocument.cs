// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ChatSessionStatusDocument.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Common.Core.Common.CommonMongoDocuments;

namespace MoneybaseTask.Business.Entities.Entities.ChatSession.MongoModel;

public class ChatSessionStatusDocument : BaseStatus
{
    public ChatSessionStatus Status { get; set; }
}