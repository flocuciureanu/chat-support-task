// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ChatSessionUserDetailsDocument.cs">
// Copyright (c) .  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneybaseTask.Business.Entities.Entities.ChatSession.MongoModel;

public class ChatSessionUserDetailsDocument
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
}