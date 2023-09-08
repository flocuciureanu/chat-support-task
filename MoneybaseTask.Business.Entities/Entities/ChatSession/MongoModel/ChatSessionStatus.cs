// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ChatSessionStatus.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Business.Entities.Entities.ChatSession.MongoModel;

public enum ChatSessionStatus
{
    //Keeping it simple for this implementation with just a few available statuses
    Pending = 10,
    InProgress = 20,
    Completed = 30,
    Closed = 40,
    Refused = 50
    
    //Examples of chat session statuses for a more complete solution
    // Abandoned,
    // Assigned,
    // Error
}